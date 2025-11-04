using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : Singleton<GameManager>
{
    protected override bool IsPersistent => false;

    private bool _isGameOver = false;

    private int _finalScore;
    private int _finalCombo;
    private int _finalMaxCombo;
    private int _finalMissCount;

    public int GetFinalScore() => _finalScore;
    public int GetFinalCombo() => _finalCombo;
    public int GetFinalMaxCombo() => _finalMaxCombo;
    public int GetFinalMissCount() => _finalMissCount;

    public void GameOver()
    {
        GetPlayerData();
    }

    public void GetPlayerData()
    {
        _finalScore = SongManager.Instance.Score;
        _finalCombo = SongManager.Instance.Combo;
        _finalMaxCombo = SongManager.Instance.MaxCombo;
        _finalMissCount = SongManager.Instance.MissCount;
    }
}