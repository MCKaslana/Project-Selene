using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;

    public event Action OnTryHitNote;
    public event Action OnKeyOnePress;
    public event Action OnKeyTwoPress;
    public event Action OnKeyThreePress;

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
    }

    private void OnDisable()
    {
        var player = _playerInput.Player;

        player.ClickNote.performed -= TryHitPerformed;
        player.SwitchKey1.performed -= SwitchOnePerformed;
        player.SwitchKey2.performed -= SwitchTwoPerformed;
        player.SwitchKey3.performed -= SwitchThreePerformed;

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
}
