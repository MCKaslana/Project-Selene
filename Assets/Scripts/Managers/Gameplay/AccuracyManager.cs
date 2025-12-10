using UnityEngine;

public class AccuracyManager : Singleton<AccuracyManager>
{
    protected override bool IsPersistent => false;

    private int totalHitValue = 0;
    private int maxHitValue = 0;

    public void RegisterJudgement(IJudgement judgement)
    {
        totalHitValue += judgement.AccuracyValue;
        maxHitValue += 100;
    }

    public int GetAccuracyPercent()
    {
        if (maxHitValue == 0) return 100;
        return Mathf.RoundToInt((float)totalHitValue / maxHitValue * 100f);
    }
}
