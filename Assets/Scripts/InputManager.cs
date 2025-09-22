using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;

    void Update()
    {
        //_playerInput = new PlayerInputs();

        //_playerInput.Player.ClickNote.performed
        //    += context => TryHitNote();

        if (Input.GetKeyDown(KeyCode.Space)) TryHitNote();
    }

    void TryHitNote()
    {
        NoteControl[] notes = FindObjectsOfType<NoteControl>(false);
        if (notes.Length == 0) return;

        float currentTime = GameManager.Instance.GetSongTime();

        NoteControl closest = notes
            .Where(n => !n.IsHit())
            .OrderBy(n => Mathf.Abs(currentTime - n.GetTargetTime()))
            .FirstOrDefault();

        if (closest != null)
        {
            bool hit = closest.TryHit();

            if (!hit)
                GameManager.Instance.RegisterHit(Judgement.Miss);
        }
    }
}
