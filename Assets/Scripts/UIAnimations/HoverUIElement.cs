using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup _uiCanvas;
    private float _fadeDuration = 1f;

    private void Awake()
    {
        gameObject.AddComponent<CanvasGroup>();
        _uiCanvas = GetComponent<CanvasGroup>();
        _uiCanvas.alpha = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiCanvas.DOFade(1f, _fadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiCanvas?.DOFade(0f, _fadeDuration);
    }
}
