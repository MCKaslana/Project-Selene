using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override bool IsPersistent => false;

    private int _finalScore;
    private int _finalCombo;
    private int _finalMaxCombo;
    private int _finalMissCount;

    // Judgement counts
    private int _perfectHits = 0;
    private int _greatHits = 0;
    private int _goodHits = 0;

    public int GetFinalScore() => _finalScore;
    public int GetFinalCombo() => _finalCombo;
    public int GetFinalMaxCombo() => _finalMaxCombo;
    public int GetFinalMissCount() => _finalMissCount;
    public int GetPerfectHits() => _perfectHits;
    public int GetGreatHits() => _greatHits;
    public int GetGoodHits() => _goodHits;
    public int GetAccuracy() => AccuracyManager.Instance.GetAccuracyPercent();

    public void UpdatePerfectHit() => _perfectHits++;
    public void UpdateGreatHit() => _greatHits++;
    public void UpdateGoodHit() => _goodHits++;

    public void GameOver()
    {
        GetPlayerData();
        PlayerData.Instance.SaveFromManager();

        var p = PlayerData.Instance;
        var diff = GameSettings.Instance.CurrentGameDifficulty.difficultyName;
        var song = GameSettings.Instance.CurrentSongData;

        LeaderboardManager.Instance.AddEntry(song.SongID, p.Score, diff, p.Accuracy, p.Combo);

        SceneManager.LoadScene("ScoreScreen");
    }

    public void GetPlayerData()
    {
        _finalScore = SongManager.Instance.Score;
        _finalCombo = SongManager.Instance.Combo;
        _finalMaxCombo = SongManager.Instance.MaxCombo;
        _finalMissCount = SongManager.Instance.MissCount;
    }
}