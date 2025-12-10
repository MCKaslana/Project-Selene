using UnityEngine;

public class SFXPlayer : Singleton<SFXPlayer>
{
    [SerializeField] private AudioSource _source;

    [Header("Sounds")]
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _miss;

    private float Volume => GameSettings.Instance.VolumeLevel;

    public void PlayHit() => _source.PlayOneShot(_hit, Volume);
    public void PlayMiss() => _source.PlayOneShot(_miss, Volume);
}
