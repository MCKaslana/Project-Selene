using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [Header("Song Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TextAsset beatmap;
    [SerializeField] private float songOffset = 0f;

    private float songStartTime;
    private List<float> noteTimings = new List<float>();
    private int nextNoteIndex = 0;

    [SerializeField] private NoteSpawner noteSpawner;

    [Header("Game Stats")]
    public int score { get; private set; }
    public int combo { get; private set; }
    public int maxCombo { get; private set; }
    public int misses { get; private set; }

    void Start()
    {
        LoadBeatmap();
        Invoke(nameof(StartSong), 1f);
    }

    void Update()
    {
        if (musicSource.isPlaying && nextNoteIndex < noteTimings.Count)
        {
            float songTime = GetSongTime();

            if (songTime >= noteTimings[nextNoteIndex] - noteSpawner.NoteTravelTime)
            {
                noteSpawner.SpawnNote(noteTimings[nextNoteIndex]);
                nextNoteIndex++;
            }
        }
    }

    public float GetSongTime()
    {
        return (Time.time - songStartTime) + songOffset;
    }

    void StartSong()
    {
        songStartTime = Time.time;
        musicSource.Play();
    }

    void LoadBeatmap()
    {
        string[] lines = beatmap.text.Split('\n');
        foreach (string line in lines)
        {
            if (float.TryParse(line, out float t))
                noteTimings.Add(t);
        }
    }

    public void RegisterHit(string judgment)
    {
        switch (judgment)
        {
            case "Perfect":
                score += 1000;
                combo++;
                break;
            case "Great":
                score += 500;
                combo++;
                break;
            case "Good":
                score += 300;
                combo++;
                break;
            case "Miss":
                combo = 0;
                misses++;
                break;
        }

        if (combo > maxCombo)
            maxCombo = combo;

        UserInterface.Instance.UpdateInterface(score, combo, misses);
    }
}
