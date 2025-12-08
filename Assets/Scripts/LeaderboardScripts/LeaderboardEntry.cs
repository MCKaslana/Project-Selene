using System;
using UnityEngine;

[Serializable]
public class LeaderboardEntry
{
    public int score;
    public string difficulty;
    public int accuracy;

    public LeaderboardEntry(int score, string difficulty, int accuracy)
    {
        this.score = score;
        this.difficulty = difficulty;
        this.accuracy = accuracy;
    }
}
