using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [Header("Song Settings")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private TextAsset _beatmap;
    [SerializeField] private float _songOffset = 0f;

    private float _songStartTime;
    private List<float> noteTimings = new();
    private int _nextNoteIndex = 0;

    [SerializeField] private NoteSpawner _noteSpawner;

    [Header("Game Stats")]
    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }
    public int MissCount { get; private set; }

    void Start()
    {
        LoadBeatmap();
        Invoke(nameof(StartSong), 1f);
    }

    void Update()
    {
        if (_musicSource.isPlaying && _nextNoteIndex < noteTimings.Count)
        {
            float songTime = GetSongTime();

            if (songTime >= noteTimings[_nextNoteIndex] - _noteSpawner.NoteTravelTime)
            {
                _noteSpawner.SpawnNote(noteTimings[_nextNoteIndex]);
                _nextNoteIndex++;
            }
        }
    }

    public float GetSongTime()
    {
        return (Time.time - _songStartTime) + _songOffset;
    }

    void StartSong()
    {
        _songStartTime = Time.time;
        _musicSource.Play();
    }

    void LoadBeatmap()
    {
        string[] lines = _beatmap.text.Split('\n');

        MaxCombo = _beatmap.text.Length;

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
                Score += 4000;
                Combo++;
                break;
            case Judgement.Great:
                Score += 2000;
                Combo++;
                break;
            case Judgement.Good:
                Score += 1000;
                Combo++;
                break;
            case Judgement.Miss:
                Score -= 1000;
                Combo = 0;
                MissCount++;
                break;
        }

        if (Combo > MaxCombo)
            MaxCombo = Combo;

        UserInterface.Instance.UpdateInterface(Score, Combo, MissCount);
    }
}