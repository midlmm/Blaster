using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterMovements Movements { get; private set; }
    public CameraRotate CameraRotate { get; private set; }

    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Transform _cameraTransform;

    private PlayerMovementsInput _movementsInput;
    private CameraRotateInput _cameraRotateInput;

    private void Awake()
    {
        Initialized();
    }

    public void Initialized()
    {
        _movementsInput = new PlayerMovementsInput();
        Movements = new CharacterMovements(_movementsInput, _characterTransform);

        _cameraRotateInput = new CameraRotateInput();
        CameraRotate = new CameraRotate(_cameraRotateInput, _characterTransform, _cameraTransform);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movements.Tick(Time.deltaTime);
        CameraRotate.Tick();
    }
}
