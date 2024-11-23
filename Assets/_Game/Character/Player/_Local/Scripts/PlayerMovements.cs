using UnityEngine;

public class PlayerMovements
{
    public CharacterMovements Movements { get; private set; }
    public CameraRotate CameraRotate { get; private set; }
    public CameraShake CameraShake { get; private set; }

    private ICharacterMovementsInput _movementsInput;
    private ICameraRotateInput _cameraRotateInput;

    private bool _isMoving;
    private float _speed;

    private float _absorbingShaking = 20f;

    public PlayerMovements(ICharacterMovementsInput movementsInput, ICameraRotateInput cameraRotateInput, Transform characterTransform, Transform cameraTransform)
    {
        _movementsInput = movementsInput;
        _cameraRotateInput = cameraRotateInput;

        Movements = new CharacterMovements(_movementsInput, characterTransform);
        CameraRotate = new CameraRotate(cameraRotateInput, characterTransform, cameraTransform);
        CameraShake = new CameraShake(cameraTransform);

        Movements.OnSwitchWalking += SwitchWalking;
        Movements.OnLanding += Landing;
    }

    public void Tick()
    {
        Movements.Tick(Time.deltaTime);

        _movementsInput.Tick();
        _cameraRotateInput.Tick();

        ShakeWalking();
    }

    public void OnDestoy()
    {
        Movements.OnDestroy();
        CameraRotate.OnDestroy();

        Movements.OnSwitchWalking -= SwitchWalking;
        Movements.OnLanding -= Landing;
    }

    private void Landing(float height)
    {
        CameraShake.Shake(height / _absorbingShaking);
    }

    private void SwitchWalking(bool isMoving, float speed)
    {
        _isMoving = isMoving;
        _speed = speed;
    }

    private void ShakeWalking()
    {
        if(!_isMoving)
            return;

        CameraShake.Swaying(_speed);
    }
}
