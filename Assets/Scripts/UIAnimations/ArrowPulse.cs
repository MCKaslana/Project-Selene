using DG.Tweening;
using UnityEngine;

public class ArrowPulse : MonoBehaviour
{
    [SerializeField] private RectTransform _leftArrow;
    [SerializeField] private RectTransform _rightArrow;

    [SerializeField] private float _moveAmount = 10f;
    [SerializeField] private float _duration = 0.4f;

    private Vector2 _leftStart;
    private Vector2 _rightStart;

    private void Start()
    {
        _leftStart = _leftArrow.anchoredPosition;
        _rightStart = _rightArrow.anchoredPosition;

        Animate();
    }

    private void Animate()
    {
        _leftArrow.DOAnchorPos(_leftStart + Vector2.right * _moveAmount, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        _rightArrow.DOAnchorPos(_rightStart + Vector2.left * _moveAmount, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
