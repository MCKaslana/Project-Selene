using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private SongData _song;
    [SerializeField] private Transform _entryParent;
    [SerializeField] private GameObject _entryPrefab;

    public void SetSong(SongData s)
    {
        _song = s;
        LoadEntries();
    }

    private void LoadEntries()
    {
        foreach (Transform t in _entryParent)
            Destroy(t.gameObject);

        if (_song == null) return;

        var data = LeaderboardManager.Instance.Load(_song.SongID);

        foreach (var e in data.entries)
        {
            var obj = Instantiate(_entryPrefab, _entryParent);
            obj.GetComponent<TextMeshProUGUI>().text =
                e.score + " | " + e.difficulty + " | " + e.accuracy + "%";
        }
    }
}
