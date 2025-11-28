using UnityEngine;

public class MissJudgement : IJudgement
{
    public void RegisterHit(SongManager manager, float multiplier)
    {
        int scoreToAdd = Mathf.RoundToInt(-500 * multiplier);
        manager.IncreaseScore(scoreToAdd);
        manager.ResetCombo();
        manager.IncreaseMissCount();
        manager.ShowPopup("Miss!", Color.red);
    }
}
