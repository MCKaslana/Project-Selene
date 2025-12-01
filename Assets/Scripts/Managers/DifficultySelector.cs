using TMPro;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    [Header("Difficulties")]
    [SerializeField] private DifficultyData[] difficulties;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI difficultyLabel;

    private int selectedIndex = 0;

    private void Start()
    {
        UpdateUI();
        ApplyDifficulty();
    }

    public void SelectDifficulty(int index)
    {
        if (index < 0 || index >= difficulties.Length)
            return;

        selectedIndex = index;
        UpdateUI();
        ApplyDifficulty();
    }

    private void UpdateUI()
    {
        DifficultyData data = difficulties[selectedIndex];
        difficultyLabel.text = $"{data.difficultyName}  (x{data.ScoreMultiplier})";
    }

    private void ApplyDifficulty()
    {
        GameSettings.Instance.SetGameDifficulty(difficulties[selectedIndex]);
    }
}
