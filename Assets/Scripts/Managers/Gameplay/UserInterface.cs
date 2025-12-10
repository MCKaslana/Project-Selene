using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _missCountText;
    [SerializeField] private TextMeshProUGUI _accuracyText;

    private void Start()
    {
        if (SongManager.Instance != null)
        {
            SongManager.Instance.OnScoreUpdate += UpdateScore;
            SongManager.Instance.OnComboUpdate += UpdateCombo;
            SongManager.Instance.OnMissUpdate += UpdateMissCount;
            SongManager.Instance.OnAccuracyUpdate += UpdateAccuracy;
        }

        UpdateScore(0);
        UpdateCombo(0);
        UpdateMissCount(0);
        UpdateAccuracy(100);
    }

    private void OnDestroy()
    {
        if (SongManager.Instance != null)
        {
            SongManager.Instance.OnScoreUpdate -= UpdateScore;
            SongManager.Instance.OnComboUpdate -= UpdateCombo;
            SongManager.Instance.OnMissUpdate -= UpdateMissCount;
            SongManager.Instance.OnAccuracyUpdate -= UpdateAccuracy;
        }
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = $"<| {score} |>";
    }

    private void UpdateCombo(int combo)
    {
        _comboText.text = $"Combo : > {combo} <";
    }

    private void UpdateMissCount(int missCount)
    {
        _missCountText.text = $"X : < {missCount} >";
    }

    private void UpdateAccuracy(int accuracy)
    {
        _accuracyText.text = $"~ {accuracy}% ~";
    }
}