using System;
using UnityEngine;

public class CameraRotateInput : ICameraRotateInput
{
    public event Action<Vector2> OnChangeMouseInput;

    private Vector2 _currentMouseInput;

    public void Tick()
    {
        MouseInput();
    }

    public void MouseInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        var mouseInput = new Vector2(x, y);

        if (_currentMouseInput != mouseInput)
            OnChangeMouseInput?.Invoke(mouseInput);

        _currentMouseInput = mouseInput;
    }
}
