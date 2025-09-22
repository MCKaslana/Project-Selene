using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [Header("Song Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TextAsset beatmap;
    [SerializeField] private float songOffset = 0f;

    private float songStartTime;
    private List<float> noteTimings = new();
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

        maxCombo = beatmap.text.Length;

        foreach (string line in lines)
        {
            if (float.TryParse(line, out float noteTiming))
                noteTimings.Add(noteTiming);
        }
    }

    public void RegisterHit(Judgement judgment)
    {
        switch (judgment)
        {
            case Judgement.Perfect:
                score += 4000;
                combo++;
                break;
            case Judgement.Great:
                score += 2000;
                combo++;
                break;
            case Judgement.Good:
                score += 1000;
                combo++;
                break;
            case Judgement.Miss:
                score -= 1000;
                combo = 0;
                misses++;
                break;
        }

        if (combo > maxCombo)
            maxCombo = combo;

        UserInterface.Instance.UpdateInterface(score, combo, misses);
    }
}