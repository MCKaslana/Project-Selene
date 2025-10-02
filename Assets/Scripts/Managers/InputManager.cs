using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInputs();

        _playerInput.Player.ClickNote.performed
            += context => TryHitNote();
        /*_playerInput.Player.SwitchKey1.performed
            += context => 
        _playerInput.Player.SwitchKey2.performed
            += context => */

        _playerInput.Enable();
    }

    void TryHitNote()
    {
        NoteControl[] notes = FindObjectsByType<NoteControl>(FindObjectsSortMode.None);
        if (notes.Length == 0) return;

        float currentTime = SongManager.Instance.GetSongTime();

        NoteControl closest = notes
            .Where(n => !n.IsHit())
            .OrderBy(n => Mathf.Abs(currentTime - n.GetTargetTime()))
            .FirstOrDefault();

        if (closest != null)
        {
            bool hit = closest.TryHit();

            if (!hit)
                SongManager.Instance.RegisterHit(Judgement.Miss);
        }
    }
}
