using UnityEngine;
using System.IO;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    protected override bool IsPersistent => false;

    private int _maxEntries = 9;

    private string GetPath(string songID)
    {
        return Path.Combine(Application.persistentDataPath, $"leaderboard_{songID}.json");
    }

    public LeaderboardData Load(string songID)
    {
        string path = GetPath(songID);
        if (!File.Exists(path))
        {
            var d = new LeaderboardData();
            Save(songID, d);
            return d;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    public void Save(string songID, LeaderboardData data)
    {
        string path = GetPath(songID);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public void AddEntry(string songID, string rank, int score, string difficulty, int accuracy, int combo)
    {
        var data = Load(songID);
        data.entries.Add(new LeaderboardEntry(rank, score, difficulty, accuracy, combo));
        data.entries.Sort((a, b) => b.score.CompareTo(a.score));

        if (data.entries.Count > _maxEntries)
            data.entries.RemoveRange(_maxEntries, data.entries.Count - _maxEntries);

        Save(songID, data);
    }
}
