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

    public void Initialize(float targetTime, Vector3 targetPos, float travelTime)
    {
        _targetTime = targetTime;
        _targetPos = targetPos;
        _travelTime = travelTime;
        _startPos = transform.position;

        if (!activeNotes.Contains(this))
            activeNotes.Add(this);
    }

    void Update()
    {
        float songTime = GameManager.Instance.GetSongTime();

        float travelProgress = 1f - ((_targetTime - songTime) / _travelTime);
        travelProgress = Mathf.Clamp01(travelProgress);
        transform.position = Vector2.Lerp(_startPos, _targetPos, travelProgress);

        if (!_hit && songTime - _targetTime > 0.2f)
        {
            _hit = true; 
            GameManager.Instance.RegisterHit(Judgement.Miss);
            Destroy(gameObject);
        }
    }

    public bool TryHit()
    {
        if (_hit) return false;

        float songTime = GameManager.Instance.GetSongTime();
        float diff = Mathf.Abs(songTime - _targetTime);

        if (diff <= 0.05f)
        {
            GameManager.Instance.RegisterHit(Judgement.Perfect);
        }
        else if (diff <= 0.1f)
        {
            GameManager.Instance.RegisterHit(Judgement.Great);
        }
        else if (diff <= 0.2f)
        {
            GameManager.Instance.RegisterHit(Judgement.Good);
        }
        else
        {
            return false;
        }

        _hit = true;
        Destroy(gameObject);
        return true;
    }

    public float GetTargetTime() => _targetTime;
    public bool IsHit() => _hit;

    void OnDestroy()
    {
        activeNotes.Remove(this);
    }
}
