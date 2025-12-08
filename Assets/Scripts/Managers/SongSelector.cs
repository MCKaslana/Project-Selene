using UnityEngine;

public class SongSelector : MonoBehaviour
{
    [Header("Songs")]
    [SerializeField] private SongData[] _songs;

    private int _selectedIndex = 0;

    public void SelectSong(int index)
    {
        if (index < 0 || index >= _songs.Length)
            return;

        _selectedIndex = index;
        ApplySong();
    }

    private void ApplySong()
    {
        GameSettings.Instance.SetSongData(_songs[_selectedIndex]);
    }
}
