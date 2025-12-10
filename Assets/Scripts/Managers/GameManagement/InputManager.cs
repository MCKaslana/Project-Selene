using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;

    public event Action OnTryHitNote;

    public event Action OnKeyOnePress;
    public event Action OnKeyTwoPress;
    public event Action OnKeyThreePress;
    public event Action OnKeyFourPress;
    public event Action OnKeyFivePress;
    public event Action OnKeySixPress;
    public event Action OnKeySevenPress;

    public event Action OnPausePress;

    private void Awake()
    {
        _playerInput = new PlayerInputs();

        _playerInput.Enable();
    }

    private void OnEnable()
    {
        var player = _playerInput.Player;

        player.ClickNote.performed += TryHitPerformed;
        player.SwitchKey1.performed += SwitchOnePerformed;
        player.SwitchKey2.performed += SwitchTwoPerformed;
        player.SwitchKey3.performed += SwitchThreePerformed;
        player.SwitchKey4.performed += SwitchFourPerformed;
        player.SwitchKey5.performed += SwitchFivePressed;
        player.SwitchKey6.performed += SwitchSixPressed;
        player.SwitchKey7.performed += SwitchSevenPressed;

        player.Pause.performed += PausePressed;
    }

    private void OnDisable()
    {
        var player = _playerInput.Player;

        player.ClickNote.performed -= TryHitPerformed;
        player.SwitchKey1.performed -= SwitchOnePerformed;
        player.SwitchKey2.performed -= SwitchTwoPerformed;
        player.SwitchKey3.performed -= SwitchThreePerformed;
        player.SwitchKey4.performed -= SwitchFourPerformed;
        player.SwitchKey5.performed -= SwitchFivePressed;
        player.SwitchKey6.performed -= SwitchSixPressed;
        player.SwitchKey7.performed -= SwitchSevenPressed;

        player.Pause.performed -= PausePressed;

        _playerInput.Disable();
    }

    private void TryHitPerformed(InputAction.CallbackContext ctx)
        => OnTryHitNote?.Invoke();

    private void SwitchOnePerformed(InputAction.CallbackContext ctx)
        => OnKeyOnePress?.Invoke();

    private void SwitchTwoPerformed(InputAction.CallbackContext ctx)
        => OnKeyTwoPress?.Invoke();

    private void SwitchThreePerformed(InputAction.CallbackContext ctx)
        => OnKeyThreePress?.Invoke();

    private void SwitchFourPerformed(InputAction.CallbackContext ctx)
        => OnKeyFourPress?.Invoke();

    private void SwitchFivePressed(InputAction.CallbackContext ctx)
        => OnKeyFivePress?.Invoke();

    private void SwitchSixPressed(InputAction.CallbackContext ctx)
        => OnKeySixPress?.Invoke();

    private void SwitchSevenPressed(InputAction.CallbackContext ctx)
        => OnKeySevenPress?.Invoke();

    private void PausePressed(InputAction.CallbackContext ctx)
        => OnPausePress?.Invoke();
}
