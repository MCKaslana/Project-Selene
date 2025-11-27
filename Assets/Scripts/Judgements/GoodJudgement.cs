using UnityEngine;

public class GoodJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        GameManager.Instance.UpdateGoodHit();
        manager.IncreaseScore(1000);
        manager.IncreaseCombo();
        manager.ShowPopup("Good!", Color.green);
    }
}
