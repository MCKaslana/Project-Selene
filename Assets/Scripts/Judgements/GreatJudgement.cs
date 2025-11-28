using UnityEngine;

public class GreatJudgement : IJudgement
{
    public void RegisterHit(SongManager manager, float multiplier)
    {
        GameManager.Instance.UpdateGreatHit();
        int scoreToAdd = Mathf.RoundToInt(2000 * multiplier);
        manager.IncreaseScore(scoreToAdd);
        manager.IncreaseCombo();
        manager.ShowPopup("Great!", Color.cyan);
    }
}
