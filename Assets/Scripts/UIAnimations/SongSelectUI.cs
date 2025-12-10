using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectUI : MonoBehaviour
{
    [Header("Song List")]
    public Button[] songButtons;

    [Header("Highlight Arrow")]
    public RectTransform highlightArrow;

    [Header("Animation")]
    public float moveDuration = 0.25f;
    public float selectedScale = 1.15f;
    public float normalScale = 1f;

    private RectTransform _currentSelected;

    private void Start()
    {
        foreach (var bbutton in songButtons)
        {
            var t = bbutton.GetComponent<RectTransform>();
            bbutton.onClick.AddListener(() => SelectSong(t));
            t.localScale = Vector3.one * normalScale;
        }

        highlightArrow.gameObject.SetActive(false);
    }

    private void SelectSong(RectTransform rectTransform)
    {
        if (_currentSelected == rectTransform)
            return;

        if (_currentSelected != null)
            _currentSelected.DOScale(normalScale, 0.2f);

        _currentSelected = rectTransform;

        if (!highlightArrow.gameObject.activeSelf)
            highlightArrow.gameObject.SetActive(true);

        highlightArrow
            .DOMoveY(rectTransform.position.y, moveDuration)
            .SetEase(Ease.OutQuad);

        rectTransform.DOScale(selectedScale, 0.25f).SetEase(Ease.OutBack);
    }
}
