using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;

    [Header("ShapeKeys")]
    [SerializeField] private List<ShapeKeyObject> _shapeKeys;
    [SerializeField] private Transform _shapeKeySpawn;

    private ShapeKeyObject _currentShapeKey;

    private int _activeShapeKeyIndex = 0;

    private void Awake()
    {
        _playerInput = new PlayerInputs();

        _playerInput.Player.ClickNote.performed
            += context => TryHitNote();

        _playerInput.Player.SwitchKey1.performed
            += context => ToggleShapeKey(0);
        _playerInput.Player.SwitchKey2.performed
            += context => ToggleShapeKey(1);
        _playerInput.Player.SwitchKey3.performed
            += context => ToggleShapeKey(2);

        _playerInput.Player.Enable();
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
