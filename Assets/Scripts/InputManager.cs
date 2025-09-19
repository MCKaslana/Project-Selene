using System;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputs();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TryHitNote();
        }
    }
}
