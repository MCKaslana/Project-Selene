using System.Collections.Generic;
using UnityEngine;

public class NoteControl : MonoBehaviour
{
    private static readonly List<NoteControl> activeNotes = new List<NoteControl>();
    public static IReadOnlyList<NoteControl> ActiveNotes => activeNotes;

    private float targetTime;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float travelTime;
    private bool hit = false;

    public void Initialize(float targetTime, Vector3 targetPos, float travelTime)
    {
        this.targetTime = targetTime;
        this.targetPos = targetPos;
        this.travelTime = travelTime;
        this.startPos = transform.position;

        // register in active list
        if (!activeNotes.Contains(this))
            activeNotes.Add(this);
    }

    void Update()
    {
        float songTime = GameManager.Instance.GetSongTime();

        float t = 1f - ((targetTime - songTime) / travelTime);
        t = Mathf.Clamp01(t);
        transform.position = Vector3.Lerp(startPos, targetPos, t);

        if (!hit && songTime - targetTime > 0.2f)
        {
            hit = true; 
            GameManager.Instance.RegisterHit(Judgement.Miss);
            Destroy(gameObject);
        }
    }

    public bool TryHit()
    {
        if (hit) return false;

        float songTime = GameManager.Instance.GetSongTime();
        float diff = Mathf.Abs(songTime - targetTime);

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

        hit = true;
        Destroy(gameObject);
        return true;
    }

    public float GetTargetTime() => targetTime;
    public bool IsHit() => hit;

    void OnDestroy()
    {
        activeNotes.Remove(this);
    }
}
