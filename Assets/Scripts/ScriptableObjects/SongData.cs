using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "BeatmapData/SongData")]
public class SongData : ScriptableObject
{
    public TextAsset TimingData;
    public AudioClip MusicTrack;
}
