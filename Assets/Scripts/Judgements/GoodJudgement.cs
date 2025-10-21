using UnityEngine;

public class GoodJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        manager.IncreaseScore(1000);
        manager.IncreaseCombo();
    }
}
