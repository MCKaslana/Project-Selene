using UnityEngine;

public class MissJudgement : IJudgement
{
    public void RegisterHit(SongManager manager)
    {
        manager.IncreaseScore(-500);
        manager.ResetCombo();
        manager.IncreaseMissCount();
        manager.ShowPopup("Miss!", Color.red);
    }
}
