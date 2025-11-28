using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "Difficulties/DifficultyData")]
public class DifficultyData : ScriptableObject
{
    public float ScoreMultiplier = 1.0f;
    public ShapeKeyObject[] shapeKeys;
}
