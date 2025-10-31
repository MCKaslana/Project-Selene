using UnityEngine;

public class PerfectJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        manager.IncreaseScore(4000);
        manager.IncreaseCombo();
        manager.ShowPopup("Perfect!", Color.yellow);
    }
}
