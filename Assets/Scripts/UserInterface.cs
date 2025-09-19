using UnityEngine;
using TMPro;

public class UserInterface : Singleton<UserInterface>
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _missCountText;
        
    private void Start()
    {
        UpdateInterface(0, 0, 0);
    }

    public void UpdateInterface(int score, int combo, int missCount)
    {
        _scoreText.text = $"Score : {score}";
        _comboText.text = $"Combo : {combo}";
        _missCountText.text = $"Misses : <{missCount}>";
    }
}