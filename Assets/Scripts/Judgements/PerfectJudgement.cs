using UnityEngine;

public class PerfectJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        GameManager.Instance.UpdatePerfectHit();
        manager.IncreaseScore(4000);
        manager.IncreaseCombo();
        manager.ShowPopup("Perfect!", Color.yellow);
    }
}
