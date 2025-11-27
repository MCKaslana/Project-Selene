using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _perfectValue;
    [SerializeField] private TextMeshProUGUI _greatValue;
    [SerializeField] private TextMeshProUGUI _goodValue;
    [SerializeField] private TextMeshProUGUI _missValue;
    [SerializeField] private TextMeshProUGUI _maxComboValue;
    [SerializeField] private TextMeshProUGUI _accuracyValue;

    private void Start()
    {
        var data = PlayerData.Instance;

        if (data == null)
        {
            Debug.LogWarning("[ScoreBoard] No PlayerDataManager found.");
            return;
        }

        _scoreText.text = "Score: " + data.Score;
        _perfectValue.text = "Perfect: " + data.Perfects;
        _greatValue.text = "Great: " + data.Greats;
        _goodValue.text = "Good: " + data.Goods;
        _missValue.text = "Misses: " + data.MissCount;
        _accuracyValue.text = "% " + data.Accuracy;
    }
}
