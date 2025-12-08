using UnityEngine;
using System.IO;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
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

    public void AddEntry(string songID, int score, string difficulty, int accuracy)
    {
        var data = Load(songID);
        data.entries.Add(new LeaderboardEntry(score, difficulty, accuracy));
        data.entries.Sort((a, b) => b.score.CompareTo(a.score));

        if (data.entries.Count > 15)
            data.entries.RemoveRange(15, data.entries.Count - 15);

        Save(songID, data);
    }
}
