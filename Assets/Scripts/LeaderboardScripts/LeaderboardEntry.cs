using System;
using UnityEngine;

[Serializable]
public class LeaderboardEntry
{
    public string rank;
    public int score;
    public string difficulty;
    public int accuracy;
    public int combo;

    public LeaderboardEntry(string rank, int score, string difficulty, int accuracy, int combo)
    {
        this.rank = rank;
        this.score = score;
        this.difficulty = difficulty;
        this.accuracy = accuracy;
        this.combo = combo;
    }
}
