using System;
using UnityEngine;

[Serializable]
public class LeaderboardEntry
{
    public int score;
    public string difficulty;
    public int accuracy;
    public int combo;

    public LeaderboardEntry(int score, string difficulty, int accuracy, int combo)
    {
        this.score = score;
        this.difficulty = difficulty;
        this.accuracy = accuracy;
        this.combo = combo;
    }
}
