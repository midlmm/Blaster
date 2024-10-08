using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate
{
    private float _sensitivity = 5f;
    private float _maxAngleY = 90f;

    private Transform _characterTransform;
    private Transform _cameraTransform;

    private ICameraRotateInput _cameraRotateInput;
    private float _currentRotationY;

    public CameraRotate(ICameraRotateInput cameraRotateInput, Transform characterTransform, Transform cameraTransform)
    {
        _characterTransform = characterTransform;
        _cameraTransform = cameraTransform;

        _cameraRotateInput = cameraRotateInput;

        Subscribes();
    }

    public void Destroy()
    {
        Unsubscribes();
    }

    public void Tick()
    {
        _cameraRotateInput.Tick();
    }

    public void RotateCamera(Vector2 inputMouse)
    {
        _currentRotationY -= inputMouse.y * _sensitivity;

        _currentRotationY = Mathf.Clamp(_currentRotationY, -_maxAngleY, _maxAngleY);

        _cameraTransform.localRotation = Quaternion.Euler(_currentRotationY, 0, 0);

        _characterTransform.Rotate(_characterTransform.up * inputMouse.x * _sensitivity);
    }

    private void Subscribes()
    {
        _cameraRotateInput.OnChangeMouseInput += RotateCamera;
    }

    private void Unsubscribes()
    {
        _cameraRotateInput.OnChangeMouseInput -= RotateCamera;
    }
}
