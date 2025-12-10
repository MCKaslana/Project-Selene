using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    protected override bool IsPersistent => true;
    public DifficultyData CurrentGameDifficulty { get; private set; }
    public SongData CurrentSongData { get; private set; }
    public float VolumeLevel { get; private set; } = 0.5f;
    public float SFXVolumeLevel { get; private set; } = 0.5f;

    private const string VolumeKey = "VOLUME_KEY";
    private const string SFXVolumeKey = "SFXVOLUME_KEY";

    public override void Awake()
    {
        base.Awake();
        VolumeLevel = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        SFXVolumeLevel = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
    }

    public void SetGameDifficulty(DifficultyData difficulty)
    {
        CurrentGameDifficulty = difficulty;
    }

    public void SetSongData(SongData songData)
    {
        CurrentSongData = songData;
    }

    public void SetVolumeLevel(float volume)
    {
        VolumeLevel = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(VolumeKey, VolumeLevel);
        PlayerPrefs.Save();
    }

    public void SetSFXVolumeLevel(float volume)
    {
        SFXVolumeLevel = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolumeLevel);
        PlayerPrefs.Save();
    }
}
