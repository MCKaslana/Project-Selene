using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>
{
    protected override bool IsPersistent => false;

    [SerializeField] private TextMeshProUGUI _volumeLabel;
    [SerializeField] private Slider _volumeSlider;

    private void Start()
    {
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        GameSettings.Instance.SetVolumeLevel(_volumeSlider.value);
    }

    public void OnVolumeChanged(float value)
    {
        GameSettings.Instance.SetVolumeLevel(value);
        _volumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
