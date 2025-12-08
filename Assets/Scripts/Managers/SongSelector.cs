using UnityEngine;

public class SongSelector : MonoBehaviour
{
    [Header("Songs")]
    [SerializeField] private SongData[] _songs;

    private int _selectedIndex = 0;

    private AudioSource _previewSource;

    private void Awake()
    {
        _previewSource = GetComponent<AudioSource>();
    }

    public void SelectSong(int index)
    {
        if (index < 0 || index >= _songs.Length)
            return;

        _selectedIndex = index;
        ApplySong();
    }

    private void ApplySong()
    {
        SongData song = _songs[_selectedIndex];
        GameSettings.Instance.SetSongData(song);

        PlayPreview(song);
    }

    private void PlayPreview(SongData song)
    {
        if (_previewSource == null)
        {
            Debug.LogWarning("Preview source missing");
            return;
        }

        _previewSource.Stop();
        _previewSource.clip = song.MusicTrack;

        _previewSource.time = song.PreviewStartTime;

        _previewSource.Play();

        CancelInvoke(nameof(LoopPreview));
        Invoke(nameof(LoopPreview), song.PreviewLength);
    }

    private void LoopPreview()
    {
        SongData song = _songs[_selectedIndex];

        _previewSource.time = song.PreviewStartTime;
        Invoke(nameof(LoopPreview), song.PreviewLength);
    }
}
