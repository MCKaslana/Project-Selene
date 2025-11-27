using UnityEngine;

public class GreatJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        GameManager.Instance.UpdateGreatHit();
        manager.IncreaseScore(2000);
        manager.IncreaseCombo();
        manager.ShowPopup("Great!", Color.cyan);
    }
}
