using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private SongData _song;
    [SerializeField] private Transform _entryParent;
    [SerializeField] private GameObject _entryPrefab;

    [Header("Animation Settings")]
    [SerializeField] private float slideDistance = 200f;
    [SerializeField] private float slideDuration = 0.25f;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private float typeSpeed = 0.015f;
    [SerializeField] private float delayBetweenEntries = 0.05f;

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

        StartCoroutine(SpawnAnimatedEntries(data));
    }

    private IEnumerator SpawnAnimatedEntries(LeaderboardData data)
    {
        float delay = 0f;

        foreach (var e in data.entries)
        {
            var obj = Instantiate(_entryPrefab, _entryParent);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_entryParent as RectTransform);
            yield return null;

            var rect = obj.GetComponent<RectTransform>();
            var text = obj.GetComponentInChildren<TextMeshProUGUI>();
            var cg = obj.AddComponent<CanvasGroup>();

            string fullText =
                "* " + e.rank + " *" + e.score + "    |    " + e.difficulty + "    |    " + "x " + e.combo + "    |    " + e.accuracy + "%";

            cg.alpha = 0f;

            Vector2 finalPos = rect.anchoredPosition;
            rect.anchoredPosition = finalPos + new Vector2(slideDistance, 0);

            DOTween.Sequence()
                .AppendInterval(delay)
                .Append(cg.DOFade(1f, fadeDuration))
                .Join(rect.DOAnchorPos(finalPos, slideDuration).SetEase(Ease.OutCubic))
                .AppendCallback(() => StartCoroutine(TypeText(text, fullText)))
                .Play();

            delay += delayBetweenEntries;
        }
    }

    private IEnumerator TypeText(TextMeshProUGUI text, string full)
    {
        text.text = "";

        for (int i = 0; i < full.Length; i++)
        {
            text.text = full.Substring(0, i + 1);
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
