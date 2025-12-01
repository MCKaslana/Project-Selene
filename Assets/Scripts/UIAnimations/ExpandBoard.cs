using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ExpandBoard : MonoBehaviour
{
    [Header("Board Components")]
    [SerializeField] private RectTransform _topSection;
    [SerializeField] private RectTransform _bottomSection;
    [SerializeField] private float _animationDuration;

    [Header("Display Information")]
    [SerializeField] private GameObject[] _displayObjects;
    [SerializeField] private float _displayWaitTime;

    [Header("Open Positions")]
    [SerializeField] private Transform _topOpenPosition;
    [SerializeField] private Transform _bottomOpenPosition;

    private Vector2 _originalTopPosition;
    private Vector2 _originalBottomPosition;

    private CanvasGroup _board;

    private void Awake()
    {
        _board = GetComponent<CanvasGroup>();
        _originalTopPosition = _topSection.anchoredPosition;
        _originalBottomPosition = _bottomSection.anchoredPosition;

        foreach (var obj in _displayObjects)
        {
            obj.AddComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void Start()
    {
        _topSection.DOMoveY(_topOpenPosition.position.y, _animationDuration);
        _bottomSection.DOMoveY(_bottomOpenPosition.position.y, _animationDuration);

        StartCoroutine(StartBoardFadeIn());
    }

    private IEnumerator StartBoardFadeIn()
    {
        yield return new WaitForSeconds(_animationDuration);
        _board.DOFade(1, _animationDuration);
        StartCoroutine(StarObjectDisplay());
    }

    private IEnumerator StarObjectDisplay()
    {
        yield return new WaitForSeconds(_animationDuration);

        foreach (var obj in _displayObjects)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1, 0.5f);
            yield return new WaitForSeconds(_displayWaitTime);
        }
    }
}