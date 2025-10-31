using System;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Note Prefab")]
    [SerializeField] GameObject notePrefab;

    [Header("PositionalData")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform hitLine;

    [Header("Shape Keys")]
    [SerializeField] private ShapeKeyObject[] _shapeKeys;

    public float NoteTravelTime { get; private set; } = 2f;

    public void SpawnNote(float targetTime)
    {
        ShapeKey randomKey = GetRandomShapeKey();

        ShapeKeyObject keyObj = Array.Find(_shapeKeys, k => k.keyValue == randomKey);

        GameObject noteObj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        NoteControl note = noteObj.GetComponent<NoteControl>();
        note.Initialize(targetTime, hitLine.position, NoteTravelTime, randomKey);

        note.SetVisual(keyObj);
    }

    private ShapeKey GetRandomShapeKey()
    {
        Array values = Enum.GetValues(typeof(ShapeKey));
        return (ShapeKey)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}