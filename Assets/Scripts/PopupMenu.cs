using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour
{
    [SerializeField] private GameObject _popupMenu;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TogglePopupMenu);
    }

    private void TogglePopupMenu()
    {
        if (_popupMenu != null)
        {
            bool isActive = _popupMenu.activeSelf;
            _popupMenu.SetActive(!isActive);
        }
    }
}
