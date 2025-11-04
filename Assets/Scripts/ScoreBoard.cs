using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _maxComboText;
    [SerializeField] private TextMeshProUGUI _missCountText;

    private void Start()
    {
        var data = PlayerData.Instance;

        if (data == null)
        {
            Debug.LogWarning("[ScoreBoard] No PlayerDataManager found.");
            return;
        }

        _scoreText.text = $"{data.Score}";
        _comboText.text = $"{data.Combo}";
        _maxComboText.text = $"{data.MaxCombo}";
        _missCountText.text = $"{data.MissCount}";
    }
}
