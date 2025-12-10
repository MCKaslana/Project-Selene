using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    private Button _optionButton;
    private GlobalInputs _globalInputs;

    private void Start()
    {
        _globalInputs = GlobalInputs.Instance;

        _optionButton = GetComponent<Button>();
        _optionButton.onClick.AddListener(ToggleOptionsMenu);
    }

    public void ToggleOptionsMenu()
    {
        _globalInputs.ToggleSettingsMenu();
    }
}
