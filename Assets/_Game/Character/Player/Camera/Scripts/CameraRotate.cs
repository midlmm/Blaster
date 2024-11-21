using UnityEngine;

public class CameraRotate
{
    private Transform _characterTransform;
    private Transform _cameraTransform;

    private ICameraRotateInput _cameraRotateInput;
    private float _currentRotationY;
    private PlayerConfigData _playerConfig;

    public CameraRotate(ICameraRotateInput cameraRotateInput, Transform characterTransform, Transform cameraTransform, PlayerConfigData playerConfig)
    {
        _characterTransform = characterTransform;
        _cameraTransform = cameraTransform;

        _cameraRotateInput = cameraRotateInput;

        _playerConfig = playerConfig;

        Subscribes();
    }

    public void OnDestroy()
    {
        Unsubscribes();
    }

    public void Tick()
    {
        _cameraRotateInput.Tick();
    }

    public void RotateCamera(Vector2 inputMouse)
    {
        _currentRotationY -= inputMouse.y * _playerConfig.Sensitivity;

        _currentRotationY = Mathf.Clamp(_currentRotationY, -_playerConfig.MaxAngleY, _playerConfig.MaxAngleY);

        _cameraTransform.localRotation = Quaternion.Euler(_currentRotationY, 0, 0);

        _characterTransform.Rotate(_characterTransform.up * inputMouse.x * _playerConfig.Sensitivity);
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
