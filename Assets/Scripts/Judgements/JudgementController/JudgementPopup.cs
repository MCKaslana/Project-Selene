using TMPro;
using UnityEngine;
using System.Collections;

public class JudgementPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float _riseDistance = 100f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private Vector3 _startPos;

    private void Awake()
    {
        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();

        _startPos = transform.localPosition;
    }

    public void Initialize(string message, Color color)
    {
        _text.text = message;
        _text.color = color;

        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        float elapsed = 0f;
        Vector3 targetPos = _startPos + Vector3.up * _riseDistance;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / _duration;

            transform.localPosition = Vector3.Lerp(_startPos, targetPos, progress);
            _canvasGroup.alpha = _fadeCurve.Evaluate(progress);

            yield return null;
        }

        Destroy(gameObject);
    }
}
