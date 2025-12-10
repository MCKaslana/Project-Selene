using UnityEngine;

public static class RankCalculator
{
    public static string CalculateRank(RankData data, int accuracy, int missCount)
    {
        foreach (var rule in data.ranks)
        {
            if (accuracy >= rule.minAccuracy && missCount <= rule.maxMisses)
                return rule.rankName;
        }

        return "D";
    }
}
