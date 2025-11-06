using System.Collections.Generic;
using UnityEngine;

public class NoteControl : MonoBehaviour
{
    private static readonly List<NoteControl> activeNotes = new List<NoteControl>();
    public static IReadOnlyList<NoteControl> ActiveNotes => activeNotes;

    private float _targetTime;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _travelTime;
    private bool _hit = false;

    private ShapeKey _shapeKey;
    private GameObject _visualInstance;

    [Header("Icon Offset")]
    [SerializeField] private Vector3 _iconOffset = new Vector3(0f, 1f, 0f);

    public void Initialize(float targetTime, Vector3 targetPos, float travelTime, ShapeKey key)
    {
        _targetTime = targetTime;
        _targetPos = targetPos;
        _travelTime = travelTime;
        _startPos = transform.position;
        _shapeKey = key;

        if (!activeNotes.Contains(this))
            activeNotes.Add(this);
    }

    public void SetVisual(ShapeKeyObject visualKey)
    {
        if (visualKey == null || visualKey.keyObject == null)
        {
            return;
        }

        Vector3 spawnPos = transform.position + _iconOffset;
        _visualInstance = Instantiate(visualKey.keyObject, spawnPos, Quaternion.identity);

        _visualInstance.transform.SetParent(transform, true);

        var sr = _visualInstance.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder += 1;
    }

    void Update()
    {
        float songTime = SongManager.Instance.GetSongTime();

        float travelProgress = 1f - ((_targetTime - songTime) / _travelTime);
        travelProgress = Mathf.Clamp01(travelProgress);
        transform.position = Vector2.Lerp(_startPos, _targetPos, travelProgress);

        if (!_hit && songTime - _targetTime > 0.2f)
        {
            _hit = true; 
            SongManager.Instance.RegisterHit(new MissJudgement());
            Destroy(gameObject);
        }
    }

    public bool TryHit(ShapeKey playerKey)
    {
        if (playerKey != _shapeKey)
            return false;

        if (_hit) return false;

        float songTime = SongManager.Instance.GetSongTime();
        float diff = Mathf.Abs(songTime - _targetTime);

        IJudgement judgement = diff switch
        {
            <= 0.05f => new PerfectJudgement(),
            <= 0.1f => new GreatJudgement(),
            <= 0.2f => new GoodJudgement(),
            _ => new MissJudgement()
        };

        if (judgement == null)
            return false;

        SongManager.Instance.RegisterHit(judgement);

        _hit = true;
        Destroy(gameObject);
        return true;
    }

    public float GetTargetTime() => _targetTime;
    public bool IsHit() => _hit;
    private void OnDestroy() => activeNotes.Remove(this);
    }
