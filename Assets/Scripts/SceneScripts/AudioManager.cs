using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    protected override bool IsPersistent => false;
    [SerializeField] private AudioSource musicSource;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ApplyVolume();
    }

    public void ApplyVolume()
    {
        if (musicSource != null)
            musicSource.volume = GameSettings.Instance.VolumeLevel;
    }
}
