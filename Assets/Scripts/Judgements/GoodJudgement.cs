using UnityEngine;

public class GoodJudgement : IJudgement
{
    public void RegisterHit(SongManager manager, float multiplier)
    {
        GameManager.Instance.UpdateGoodHit();
        int scoreToAdd = Mathf.RoundToInt(1000 * multiplier);
        manager.IncreaseScore(scoreToAdd);
        manager.IncreaseCombo();
        manager.ShowPopup("Good!", Color.green);
    }
}
