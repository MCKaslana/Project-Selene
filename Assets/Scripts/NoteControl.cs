using UnityEngine;

public class NoteControl : MonoBehaviour
{
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
    }

    void Update()
    {
        float songTime = GameManager.Instance.GetSongTime();
        float t = 1f - ((targetTime - songTime) / travelTime);
        t = Mathf.Clamp01(t);

        transform.position = Vector3.Lerp(startPos, targetPos, t);

        // Miss detection
        if (!hit && songTime - targetTime > 0.2f) // late miss
        {
            GameManager.Instance.RegisterHit("Miss");
            Destroy(gameObject);
        }
    }

    public void TryHit()
    {
        if (hit) return;

        float songTime = GameManager.Instance.GetSongTime();
        float diff = Mathf.Abs(songTime - targetTime);

        if (diff <= 0.05f)
        {
            GameManager.Instance.RegisterHit("Perfect");
        }
        else if (diff <= 0.1f)
        {
            GameManager.Instance.RegisterHit("Great");
        }
        else if (diff <= 0.2f)
        {
            GameManager.Instance.RegisterHit("Good");
        }
        else
        {
            GameManager.Instance.RegisterHit("Miss");
        }

        hit = true;
        Destroy(gameObject);
    }
}
