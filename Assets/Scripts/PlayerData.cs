using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [Header("Player Session Data")]
    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }
    public int MissCount { get; private set; }

    public void SaveFromManager()
    {
        var gm = GameManager.Instance;

        if (gm == null) return;

        Score = gm.GetFinalScore();
        Combo = gm.GetFinalCombo();
        MaxCombo = gm.GetFinalMaxCombo();
        MissCount = gm.GetFinalMissCount();
    }
}
