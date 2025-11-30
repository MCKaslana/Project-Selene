using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ButtonMode
{
    Default,
    Slidein
}

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Button Settings")]
    [SerializeField] private ButtonMode _mode;
    [SerializeField] private float _scaleValue = 1.15f;

    private Vector2 _originalX;
    private Vector2 _originalY;

    private void Awake()
    {
        _originalX.x = transform.localPosition.x;
        _originalY.y = transform.localPosition.y;
    }

    private void OnEnable()
    {
        if (_mode == ButtonMode.Slidein) transform.DOLocalMoveX(0f, 0.7f);
    }

    private void OnDisable()
    {
        if (_mode == ButtonMode.Slidein)
        {
            transform.localPosition = new Vector2(_originalX.x, _originalY.y);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_scaleValue, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f);
    }
}
