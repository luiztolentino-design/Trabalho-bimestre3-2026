using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class P2KeyboardController : MonoBehaviour
{
    private StarterAssetsInputs _inputs;

    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        if (_inputs == null)
        {
            Debug.LogError("[P2KeyboardController] Componente StarterAssetsInputs não encontrado no P2!");
        }
    }

    private void Update()
    {
        if (_inputs == null) return;

        // Acessa o teclado diretamente pelo New Input System
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float moveX = 0f;
        float moveY = 0f;

        // MOVIMENTO COM AS SETAS
        if (keyboard.upArrowKey.isPressed) moveY += 1f;
        if (keyboard.downArrowKey.isPressed) moveY -= 1f;
        if (keyboard.leftArrowKey.isPressed) moveX -= 1f;
        if (keyboard.rightArrowKey.isPressed) moveX += 1f;

        // Aplica o vetor de movimento
        Vector2 moveVector = new Vector2(moveX, moveY).normalized;
        _inputs.MoveInput(moveVector);

        // PULO COM O 0 DO TECLADO NUMÉRICO (NUMPAD 0)
        bool isJumpPressed = keyboard.numpad0Key.isPressed;
        _inputs.JumpInput(isJumpPressed);
    }
}