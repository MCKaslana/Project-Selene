using UnityEngine;

public class PerfectJudgement : IJudgement
{
    public int AccuracyValue => 100;

    public void RegisterHit(SongManager manager, float multiplier)
    {
        GameManager.Instance.UpdatePerfectHit();
        int scoreToAdd = Mathf.RoundToInt(4000 * multiplier);
        manager.IncreaseScore(scoreToAdd);
        manager.IncreaseCombo();
        manager.ShowPopup("Perfect!", Color.yellow);
        SFXPlayer.Instance.PlayHit();
    }
}
