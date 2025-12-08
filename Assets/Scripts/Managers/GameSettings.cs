using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    protected override bool IsPersistent => true;
    public DifficultyData CurrentGameDifficulty { get; private set; }
    public SongData CurrentSongData { get; private set; }

    public void SetGameDifficulty(DifficultyData difficulty)
    {
        CurrentGameDifficulty = difficulty;
    }

    public void SetSongData(SongData songData)
    {
        CurrentSongData = songData;
    }
}
