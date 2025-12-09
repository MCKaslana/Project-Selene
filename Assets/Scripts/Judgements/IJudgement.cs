using UnityEngine;

public interface IJudgement
{
    int AccuracyValue { get; }
    void RegisterHit(SongManager manager, float multiplier);
}