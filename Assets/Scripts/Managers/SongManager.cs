using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class SongManager : Singleton<SongManager>
{
    protected override bool IsPersistent => false;

    public event Action<int> OnScoreUpdate;
    public event Action<int> OnComboUpdate;
    public event Action<int> OnMissUpdate;

    private bool _hasSongStarted = false;

    [Header("Song Settings")]
    [SerializeField] private SongData _currentSong;
    private AudioSource _musicSource;

    private AudioClip _song;
    private TextAsset _beatmap;

    [SerializeField] private float _songOffset = 0f;

    [SerializeField] private DifficultyData _difficulty;
    public DifficultyData CurrentDifficulty => _difficulty;

    private float _songStartTime;
    private List<float> noteTimings = new();
    private int _nextNoteIndex = 0;

    [Header("Note Spawner")]
    [SerializeField] private NoteSpawner _noteSpawner;

    [Header("Judgement Popup Settings")]
    [SerializeField] private JudgementPopup _popupPrefab;
    [SerializeField] private Transform _popupParent;

    [Header("Game Stats")]
    private int _score;
    private int _combo;
    private int _maxCombo;
    private int _missCount;

    public int Score => _score;
    public int Combo => _combo;
    public int MaxCombo => _maxCombo;
    public int MissCount => _missCount;

    public override void Awake()
    {
        base.Awake();

        _musicSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _difficulty = GameSettings.Instance.CurrentGameDifficulty;
        _currentSong = GameSettings.Instance.CurrentSongData;
        _noteSpawner.SetDifficulty(_difficulty);

        _song = _currentSong.MusicTrack;
        _beatmap = _currentSong.TimingData;

        LoadBeatmap();
        Invoke(nameof(StartSong), 3f);
        _maxCombo = 0;
    }

    void Update()
    {
        if (!_hasSongStarted) return;

        if (_musicSource.isPlaying && _nextNoteIndex < noteTimings.Count)
        {
            float songTime = GetSongTime();

            if (songTime >= noteTimings[_nextNoteIndex] - _noteSpawner.NoteTravelTime)
            {
                _noteSpawner.SpawnNote(noteTimings[_nextNoteIndex]);
                _nextNoteIndex++;
            }
        }

        if (_musicSource.time >= _musicSource.clip.length - 0.1f)
        {
            Debug.Log("Song Ended by time");
            StartCoroutine(BeginEndSongDelay());
        }
    }

    private IEnumerator BeginEndSongDelay()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GameOver();
    }

    public float GetSongTime()
    {
        return _musicSource.time + _songOffset;
    }

    void StartSong()
    {
        _songStartTime = Time.time;
        _musicSource.resource = _song;
        _musicSource.Play();
        _hasSongStarted = true;
    }

    void LoadBeatmap()
    {
        string[] lines = _beatmap.text.Split('\n');

        foreach (string line in lines)
        {
            if (float.TryParse(line, out float noteTiming))
                noteTimings.Add(noteTiming);
        }
    }

    public void RegisterHit(IJudgement judgement)
    {
        judgement.RegisterHit(this, _difficulty.ScoreMultiplier);

        OnScoreUpdate?.Invoke(_score);
        OnComboUpdate?.Invoke(_combo);
        OnMissUpdate?.Invoke(_missCount);
    }

    public void ShowPopup(string text, Color color)
    {
        if (_popupPrefab == null || _popupParent == null)
        {
            Debug.LogWarning("JudgementPopup prefab or parent not assigned!");
            return;
        }

        JudgementPopup popup = Instantiate(_popupPrefab, _popupParent);
        popup.Initialize(text, color);
    }

    // methods for increasing game stats

    public void IncreaseScore(int amount) => _score += amount;
    public void IncreaseCombo()
    {
        _combo++;

        if (_combo > _maxCombo)
            _maxCombo = _combo;
    }
    public void IncreaseMissCount() => _missCount++;
    public void ResetCombo()
    {
        if (_combo > _maxCombo)
            _maxCombo = _combo;

        _combo = 0;
    }
}
