using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalInputs : Singleton<GlobalInputs>
{
    protected override bool IsPersistent => true;
    private PlayerInputs _input;

    [SerializeField] private GameObject _settingsMenu;

    public override void Awake()
    {
        base.Awake();
        _input = new PlayerInputs();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.UI.ToggleSettings.performed += OnOpenSettings;
        _input.UI.CloseSettings.performed += OnCloseSettings;
    }

    private void OnDisable()
    {
        _input.UI.ToggleSettings.performed -= OnOpenSettings;
        _input.UI.CloseSettings.performed -= OnCloseSettings;
        _input.Disable();
    }

    private void OnOpenSettings(InputAction.CallbackContext ctx)
    {
        bool show = !_settingsMenu.activeSelf;
        _settingsMenu.SetActive(show);

        if (show)
        {
            _input.Player.Disable();
        }
        else
        {
            _input.Player.Enable();
        }
    }

    private void OnCloseSettings(InputAction.CallbackContext ctx)
    {
        if (_settingsMenu.activeSelf)
        {
            _settingsMenu.SetActive(false);
            _input.Player.Enable();
        }
    }
}
