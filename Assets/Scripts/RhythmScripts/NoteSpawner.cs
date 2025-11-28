using System;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Note Prefab")]
    [SerializeField] private GameObject _notePrefab;

    [Header("PositionalData")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _hitLine;

    [Header("Difficulty Setter")]
    [SerializeField] private DifficultyData _difficulty;

    public float NoteTravelTime { get; private set; } = 2f;

    public void SpawnNote(float targetTime)
    {
        if (_difficulty == null || _difficulty.shapeKeys.Length == 0)
        {
            Debug.LogWarning("Difficulty data or shape keys not set properly.");
            return;
        }

        ShapeKeyObject keyObject = GetRandomShapeKey();
        ShapeKey randomKey = keyObject.keyValue;

        GameObject noteObj = Instantiate(_notePrefab, _spawnPoint.position, Quaternion.identity);
        NoteControl note = noteObj.GetComponent<NoteControl>();
        note.Initialize(targetTime, _hitLine.position, NoteTravelTime, randomKey);

        note.SetVisual(keyObject);
    }

    private ShapeKeyObject GetRandomShapeKey()
    {
        return _difficulty.shapeKeys
            [UnityEngine.Random.Range(0, _difficulty.shapeKeys.Length)
        ];
    }
}