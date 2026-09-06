using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Configurações Multiplayer")]
        public int PlayerID = 1;
        public float BonusVelocidadePorMoeda = 0.5f;

        [Header("Player")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        public float JumpHeight = 1.2f;
        public float Gravity = -15.0f;
        public float JumpTimeout = 0.50f;
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        public bool Grounded = true;
        public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget;
        public float TopClamp = 70.0f;
        public float BottomClamp = -30.0f;
        public float CameraAngleOverride = 0.0f;
        public bool LockCameraPosition = false;
        public Vector2 LookSensitivity = new Vector2(1.5f, 1.0f);

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private Vector3 _cameraStartingLocalPosition;
        private Quaternion _cameraStartingLocalRotation;

        public bool IsRespawning { get; set; } = false;

        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        private int _moedasColetadas = 0;

        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput != null && _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }

        private void Awake()
        {
            // Busca a câmera principal de acordo com o jogador
            Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
            foreach (Camera cam in cameras)
            {
                if ((PlayerID == 1 && cam.gameObject.name.Contains("1")) ||
                    (PlayerID == 2 && cam.gameObject.name.Contains("2")))
                {
                    _mainCamera = cam.gameObject;
                    break;
                }
            }

            if (_mainCamera == null && Camera.main != null)
            {
                _mainCamera = Camera.main.gameObject;
            }
        }

        private void Start()
        {
            _cinemachineTargetYaw = transform.eulerAngles.y;
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
#endif
            VincularCinemachine();
            AssignAnimationIDs();

            if (CinemachineCameraTarget != null)
            {
                _cameraStartingLocalPosition = CinemachineCameraTarget.transform.localPosition;
                _cameraStartingLocalRotation = CinemachineCameraTarget.transform.localRotation;
            }

            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            GroundedCheck();
            JumpAndGravity();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void VincularCinemachine()
        {
            string nomeVirtualCam = (PlayerID == 1) ? "PlayerFollowCamera1" : "PlayerFollowCamera2";
            GameObject vcamObj = GameObject.Find(nomeVirtualCam);

            if (vcamObj == null && PlayerID == 1) vcamObj = GameObject.Find("PlayerFollowCamera");
            if (vcamObj == null) return;

            // Garante que o target seja o PlayerCameraRoot
            Transform rootFilho = transform.Find("PlayerCameraRoot");
            if (rootFilho != null) CinemachineCameraTarget = rootFilho.gameObject;

            // Cinemachine v3
            var vcamV3 = vcamObj.GetComponent<Unity.Cinemachine.CinemachineCamera>();
            if (vcamV3 != null)
            {
                vcamV3.Target.TrackingTarget = CinemachineCameraTarget.transform;
                vcamV3.Target.LookAtTarget = null;
                return;
            }

            // Cinemachine v2
            var vcamV2 = vcamObj.GetComponent<Unity.Cinemachine.CinemachineVirtualCamera>();
            if (vcamV2 != null)
            {
                vcamV2.Follow = CinemachineCameraTarget.transform;
                vcamV2.LookAt = null;
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
                _moedasColetadas++;

                // Aumenta a velocidade do jogador ao coletar moedas
                MoveSpeed += BonusVelocidadePorMoeda;
                SprintSpeed += BonusVelocidadePorMoeda;

                PlayerObserverManager.NotifyCoinCollected(PlayerID, _moedasColetadas);
            }
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            if (_hasAnimator) _animator.SetBool(_animIDGrounded, Grounded);
        }

        private void CameraRotation()
        {
            if (IsRespawning)
            {
                _cinemachineTargetYaw = transform.eulerAngles.y;
                _cinemachineTargetPitch = 0f;
                CinemachineCameraTarget.transform.localPosition = _cameraStartingLocalPosition;
                CinemachineCameraTarget.transform.localRotation = _cameraStartingLocalRotation;
                IsRespawning = false;
                return;
            }

            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier * LookSensitivity.x;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier * LookSensitivity.y;
            }
            // Auto-alinhamento automático com a frente do robô
            else if (_input.move.sqrMagnitude >= _threshold)
            {
                _cinemachineTargetYaw = Mathf.LerpAngle(_cinemachineTargetYaw, transform.eulerAngles.y, Time.deltaTime * 4.0f);
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if (_input.move != Vector2.zero)
            {
                float targetRotationAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                if (_mainCamera != null) targetRotationAngle += _mainCamera.transform.eulerAngles.y;

                _targetRotation = targetRotationAngle;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                _fallTimeoutDelta = FallTimeout;
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }
                if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    if (_hasAnimator) _animator.SetBool(_animIDJump, true);
                }
                if (_jumpTimeoutDelta >= 0.0f) _jumpTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                _jumpTimeoutDelta = JumpTimeout;
                if (_fallTimeoutDelta >= 0.0f) _fallTimeoutDelta -= Time.deltaTime;
                else if (_hasAnimator) _animator.SetBool(_animIDFreeFall, true);
                _input.jump = false;
            }

            if (_verticalVelocity < _terminalVelocity) _verticalVelocity += Gravity * Time.deltaTime;
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        public void ResetCameraRotation(float targetYaw)
{
    _cinemachineTargetYaw = targetYaw;
    _cinemachineTargetPitch = 0f;
    IsRespawning = true;

    if (CinemachineCameraTarget != null)
    {
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }
}
    }
}