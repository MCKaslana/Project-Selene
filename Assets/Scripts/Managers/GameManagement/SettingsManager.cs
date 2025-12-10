using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>
{
    protected override bool IsPersistent => true;

    [SerializeField] private TextMeshProUGUI _volumeLabel;
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private TextMeshProUGUI _sfxVolumeLabel;
    [SerializeField] private Slider _sfxVolumeSlider;

    public override void Awake()
    {
        base.Awake();
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        if (GameSettings.Instance == null) return;

        float saved = GameSettings.Instance.VolumeLevel;
        _volumeSlider.value = saved;
        _volumeLabel.text = Mathf.RoundToInt(saved * 100) + "%";

        float sfxSaved = GameSettings.Instance.SFXVolumeLevel;
        _sfxVolumeSlider.value = sfxSaved;
        _sfxVolumeLabel.text = Mathf.RoundToInt(sfxSaved * 100) + "%";

        _volumeSlider.onValueChanged.RemoveAllListeners();
        _sfxVolumeSlider.onValueChanged.RemoveAllListeners();

        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    public void OnVolumeChanged(float value)
    {
        GameSettings.Instance.SetVolumeLevel(value);
        _volumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
        AudioManager.Instance?.ApplyVolume();
    }

    public void OnSFXVolumeChanged(float value)
    {
        GameSettings.Instance.SetSFXVolumeLevel(value);
        _sfxVolumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
        AudioManager.Instance?.ApplyVolume();
    }
}
