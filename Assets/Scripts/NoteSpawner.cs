using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject notePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform hitLine;

    public float NoteTravelTime { get; private set; } = 2f;

    public void SpawnNote(float targetTime)
    {
        GameObject noteObj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        NoteControl note = noteObj.GetComponent<NoteControl>();
        note.Initialize(targetTime, hitLine.position, NoteTravelTime);
    }
}