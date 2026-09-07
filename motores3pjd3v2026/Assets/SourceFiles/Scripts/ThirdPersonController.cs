using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
<<<<<<< HEAD
<<<<<<< HEAD
        [Header("Configurações Multiplayer")]
        public int PlayerID = 1;
        public float BonusVelocidadePorMoeda = 0.5f;

=======
>>>>>>> parent of eff3f27 (commit2.final)
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;
<<<<<<< HEAD
        [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;
=======
        [Header("Multiplayer & Atividade")]
       
public int PlayerID = 1; 
public float BônusVelocidadePorMoeda = 0.5f; 
public Camera PlayerCamera;
        public float MoveSpeed = 2.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;
=======
>>>>>>> parent of eff3f27 (commit2.final)
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
<<<<<<< HEAD
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
>>>>>>> parent of eff3f27 (commit2.final)
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
<<<<<<< HEAD
<<<<<<< HEAD
=======
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
>>>>>>> parent of eff3f27 (commit2.final)
        public float Gravity = -15.0f;
        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;
<<<<<<< HEAD
=======
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;
        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
>>>>>>> parent of eff3f27 (commit2.final)
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;
<<<<<<< HEAD
<<<<<<< HEAD
=======
        [Tooltip("Useful for rough ground")]
>>>>>>> parent of eff3f27 (commit2.final)
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;
<<<<<<< HEAD
=======
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
        [Tooltip("What layers the character uses as ground")]
>>>>>>> parent of eff3f27 (commit2.final)
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
<<<<<<< HEAD
<<<<<<< HEAD
=======
        [Tooltip("How far in degrees can you move the camera up")]
>>>>>>> parent of eff3f27 (commit2.final)
        public float TopClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;
        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;
        public Vector2 LookSensitivity = new Vector2(7.5f, 5.0f);

<<<<<<< HEAD
=======
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;
        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;
        public Vector2 LookSensitivity = new Vector2(7.5f, 5.0f);

        // cinemachine
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
        // cinemachine
>>>>>>> parent of eff3f27 (commit2.final)
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // Camera starting position and rotation
        private Vector3 _cameraStartingPosition;
        private Quaternion _cameraStartingRotation;

<<<<<<< HEAD
        public bool IsRespawning { get; set; } = false;

<<<<<<< HEAD
=======
        // Camera starting position and rotation
        private Vector3 _cameraStartingPosition;
        private Quaternion _cameraStartingRotation;

        public bool IsRespawning { get; set; } = false;

        // player
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
        // player
>>>>>>> parent of eff3f27 (commit2.final)
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        
        // --- ADICIONADO PARA A ATIVIDADE ---
<<<<<<< HEAD
        private int _moedasColetadas = 0;
        // ------------------------------------

=======
>>>>>>> parent of eff3f27 (commit2.final)
        private int _moedasColetadas = 0;
        // ------------------------------------

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
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
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }

<<<<<<< HEAD
        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
#endif
            AssignAnimationIDs();

            _cameraStartingPosition = CinemachineCameraTarget.transform.position;
            _cameraStartingRotation = CinemachineCameraTarget.transform.rotation;

            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

=======
       private void Awake()
{
    if (PlayerCamera != null)
    {
        _mainCamera = PlayerCamera.gameObject;
    }
    else if (_mainCamera == null)
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
}
      private void Start()
{
    
    _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    _hasAnimator = TryGetComponent(out _animator);
    _controller = GetComponent<CharacterController>();
    _input = GetComponent<StarterAssetsInputs>();

#if ENABLE_INPUT_SYSTEM 
    _playerInput = GetComponent<PlayerInput>();

    if (_playerInput != null && Keyboard.current != null)
    {
        // Define qual esquema usar com base no PlayerID
        string schemeName = (PlayerID == 1) ? "Player1_Scheme" : "Player2_Scheme";

        // Força a Unity a vincular o teclado físico a este esquema específico
        _playerInput.SwitchCurrentControlScheme(schemeName, Keyboard.current);
    }
#endif

    AssignAnimationIDs();

    _cameraStartingPosition = CinemachineCameraTarget.transform.position;
    _cameraStartingRotation = CinemachineCameraTarget.transform.rotation;

    _jumpTimeoutDelta = JumpTimeout;
    _fallTimeoutDelta = FallTimeout;
}       private void Update()
{
    GroundedCheck();    // 1. Primeiro verifica se está tocando o chão
    JumpAndGravity();   // 2. Depois processa o pulo e a gravidade
    Move();             // 3. Por fim aplica o movimento
}
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
        private void LateUpdate()
        {
            CameraRotation();
        }

<<<<<<< HEAD
<<<<<<< HEAD
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

=======
>>>>>>> parent of eff3f27 (commit2.final)
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
                _moedasColetadas++;
        
                PlayerOM.UpdateCoinCount(_moedasColetadas);
            }
        }
=======
      private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Coin"))
    {
        Destroy(other.gameObject);
        _moedasColetadas++;

        // Aumenta a velocidade do jogador que coletou
        MoveSpeed += BônusVelocidadePorMoeda;
        SprintSpeed += BônusVelocidadePorMoeda;

        // Dispara a notificação via Observer
        PlayerOM.OnCoinCountChanged?.Invoke(PlayerID, _moedasColetadas);

        // Notifica o Singleton do GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarMoedaColetada(PlayerID, _moedasColetadas);
        }
    }
}
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8

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

<<<<<<< HEAD
<<<<<<< HEAD
            if (_hasAnimator) _animator.SetBool(_animIDGrounded, Grounded);
=======
=======
>>>>>>> parent of eff3f27 (commit2.final)
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
<<<<<<< HEAD
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
>>>>>>> parent of eff3f27 (commit2.final)
        }

        private void CameraRotation()
        {
            if (IsRespawning)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                _cinemachineTargetYaw = transform.eulerAngles.y;
                _cinemachineTargetPitch = 0f;
                CinemachineCameraTarget.transform.localPosition = _cameraStartingLocalPosition;
                CinemachineCameraTarget.transform.localRotation = _cameraStartingLocalRotation;
=======
                _cinemachineTargetYaw = 0f;
                _cinemachineTargetPitch = 0f;
                CinemachineCameraTarget.transform.position = _cameraStartingPosition;
                CinemachineCameraTarget.transform.rotation = _cameraStartingRotation;
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
                _cinemachineTargetYaw = 0f;
                _cinemachineTargetPitch = 0f;
                CinemachineCameraTarget.transform.position = _cameraStartingPosition;
                CinemachineCameraTarget.transform.rotation = _cameraStartingRotation;
>>>>>>> parent of eff3f27 (commit2.final)
                IsRespawning = false;
                return;
            }

            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier * LookSensitivity.x;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier * LookSensitivity.y;
            }
<<<<<<< HEAD
<<<<<<< HEAD
            // Auto-alinhamento automático com a frente do robô
            else if (_input.move.sqrMagnitude >= _threshold)
            {
                _cinemachineTargetYaw = Mathf.LerpAngle(_cinemachineTargetYaw, transform.eulerAngles.y, Time.deltaTime * 4.0f);
            }
=======
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
>>>>>>> parent of eff3f27 (commit2.final)

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
<<<<<<< HEAD
<<<<<<< HEAD
                float targetRotationAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                if (_mainCamera != null) targetRotationAngle += _mainCamera.transform.eulerAngles.y;

                _targetRotation = targetRotationAngle;
=======
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
>>>>>>> parent of eff3f27 (commit2.final)
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
<<<<<<< HEAD
<<<<<<< HEAD
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
=======

=======

>>>>>>> parent of eff3f27 (commit2.final)
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public void ResetCameraRotation(float targetYaw)
        {
            _cinemachineTargetYaw = targetYaw;
            _cinemachineTargetPitch = 0f;
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0f);
        }
<<<<<<< HEAD
>>>>>>> 17c5f33cbb18fa10b6eb2b30d74335c4eeb832c8
=======
>>>>>>> parent of eff3f27 (commit2.final)
    }
}