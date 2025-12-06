using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InGameInput : MonoBehaviour
{
    [Header("Input Reference")]
    private InputManager _input;

    [Header("ShapeKeys")]
    [SerializeField] private List<ShapeKeyObject> _shapeKeys;
    [SerializeField] private Transform _shapeKeySpawn;
    [SerializeField] private GameObject _pauseMenu;

    private ShapeKeyObject _currentShapeKey;

    private int _activeShapeKeyIndex = 0;

    private void Awake()
    {
        _input = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        _input.OnTryHitNote += TryHitNote;

        _input.OnKeyOnePress += () => ToggleShapeKey(0);
        _input.OnKeyTwoPress += () => ToggleShapeKey(1);
        _input.OnKeyThreePress += () => ToggleShapeKey(2);
        _input.OnKeyFourPress += () => ToggleShapeKey(3);
        _input.OnKeyFivePress += () => ToggleShapeKey(4);
        _input.OnKeySixPress += () => ToggleShapeKey(5);
        _input.OnKeySevenPress += () => ToggleShapeKey(6);

        _input.OnPausePress += TogglePauseMenu;
    }

    private void OnDisable()
    {
        _input.OnTryHitNote -= TryHitNote;

        _input.OnKeyOnePress -= () => ToggleShapeKey(0);
        _input.OnKeyTwoPress -= () => ToggleShapeKey(1);
        _input.OnKeyThreePress -= () => ToggleShapeKey(2);
        _input.OnKeyFourPress -= () => ToggleShapeKey(3);
        _input.OnKeyFivePress -= () => ToggleShapeKey(4);
        _input.OnKeySixPress -= () => ToggleShapeKey(5);
        _input.OnKeySevenPress -= () => ToggleShapeKey(6);

        _input.OnPausePress -= TogglePauseMenu;
    }

    private void Start()
    {
        foreach (var key in _shapeKeys)
        {
            GameObject obj = Instantiate(key.keyObject, _shapeKeySpawn.position, Quaternion.identity, _shapeKeySpawn);
            key.runTimeObject = obj;
            obj.SetActive(false);
        }

        ToggleShapeKey(0);
    }

    private void TogglePauseMenu()
    {
        if (_pauseMenu != null)
        {
            bool isActive = _pauseMenu.activeSelf;
            _pauseMenu.SetActive(!isActive);
            Time.timeScale = isActive ? 1f : 0f;
        }
    }

    private void ToggleShapeKey(int index)
    {
        if (index < 0 || index >= _shapeKeys.Count)
            return;

        _activeShapeKeyIndex = index;
        _currentShapeKey = _shapeKeys[index];

        for (int i = 0; i < _shapeKeys.Count; i++)
        {
            var key = _shapeKeys[i];
            bool isActive = i == index;
            if (key.runTimeObject != null)
                key.runTimeObject.SetActive(isActive);
        }
    }

    private void TryHitNote()
    {
        Debug.Log("HitNote");

        NoteControl[] notes = FindObjectsByType<NoteControl>(FindObjectsSortMode.None);
        if (notes.Length == 0) return;

        float currentTime = SongManager.Instance.GetSongTime();

        NoteControl closest = notes
            .Where(n => !n.IsHit())
            .OrderBy(n => Mathf.Abs(currentTime - n.GetTargetTime()))
            .FirstOrDefault();

        if (closest != null)
        {
            bool hit = closest.TryHit(_currentShapeKey.keyValue);

            if (!hit)
                SongManager.Instance.RegisterHit(new MissJudgement());
        }
    }
}
