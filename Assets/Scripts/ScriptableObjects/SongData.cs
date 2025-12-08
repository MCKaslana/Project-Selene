using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "BeatmapData/SongData")]
public class SongData : ScriptableObject
{
    public TextAsset TimingData;
    public AudioClip MusicTrack;

    [Header("Preview Settings")]
    public float PreviewStartTime = 30f;
    public float PreviewLength = 10f;
}
