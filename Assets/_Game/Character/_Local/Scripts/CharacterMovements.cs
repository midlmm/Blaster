using System;
using UnityEngine;

public class CharacterMovements
{
    public Action<bool, float> OnSwitchWalking;
    public Action OnJumping;
    public Action<float> OnLanding;
    
    private readonly CharacterConfigData _defaultCharacterConfig = Resources.Load<CharacterConfigData>("DefaultCharacterConfig");
    private readonly ICharacterMovementsInput _characterMovementsInput;
    private readonly float _characterHeight;

    private CharacterConfigData _currentCharacterConfig;
    private CharacterController _characterController;

    private Transform _characterTransform;

    private Vector3 _velocity;
    private float _currentSpeed;
    private bool _isFalling;
    private bool _isCrouching;
    private bool _isMoving;
    private float _backHeight;
    private float _dropHeight;

    public CharacterMovements(ICharacterMovementsInput characterMovementsInput, Transform characterTransform)
    {
        _characterMovementsInput = characterMovementsInput;
        _characterTransform = characterTransform;

        _characterController = characterTransform.GetComponent<CharacterController>();
        _characterHeight = _characterController.height;

        _currentCharacterConfig = _defaultCharacterConfig;
        _currentSpeed = _currentCharacterConfig.OnGroundSpeed;

        Subscribes();
    }

    public void OnDestroy()
    {
        Unsubscribes();
    }

    public void Tick(float time)
    {
        _characterMovementsInput.Tick();

        Gravity(time);
        SetVelosity(time);
        Landing();
    }

    public void SetCharacterConfig(CharacterConfigData characterConfigData)
    {
        _currentCharacterConfig = characterConfigData;
    }

    public void SetDefaultChatacterConfig()
    {
        _currentCharacterConfig = _defaultCharacterConfig;
    }

    private void SetVelosity(float time)
    {
        var direction = (_characterTransform.right * _velocity.x + _characterTransform.forward * _velocity.z) * _currentSpeed;
        direction.y = _velocity.y;

        _characterController.Move(direction * time);

        if(direction.x == 0 && direction.z == 0 && _isMoving)
        {
            _isMoving = false;
            OnSwitchWalking?.Invoke(_isMoving, _currentSpeed);
        }      
        else if(direction.x != 0 && direction.z != 0 && !_isMoving)
        {
            _isMoving = true;
            OnSwitchWalking?.Invoke(_isMoving, _currentSpeed);
        }
    }

    private void Walking(Vector2 inputValue)
    {
        _velocity = new Vector3(inputValue.x, _velocity.y, inputValue.y);
    }

    private void Runing(bool isActive)
    {
        if (_isCrouching)
            return;

        if (!_characterController.isGrounded || _isFalling)
            isActive = false;
        _currentSpeed = isActive ? _currentCharacterConfig.OnGroundSpeed * _currentCharacterConfig.OnAccelerate : _currentCharacterConfig.OnGroundSpeed;
    }

    private void Crouching(bool isActive)
    {
        if (!_characterController.isGrounded || _isFalling)
            isActive = false;

        var newSpeed = _currentCharacterConfig.OnGroundSpeed;
        var newHeight = _characterHeight;

        _isCrouching = isActive;

        if (isActive)
        {
            newSpeed = _currentCharacterConfig.OnGroundSpeed * _currentCharacterConfig.OnDecelerate;
            newHeight = _currentCharacterConfig.CrouchHeight;
        }

        _currentSpeed = newSpeed;
        _characterController.height = newHeight;
    }

    private void Jumping()
    {
        if (!_characterController.isGrounded)
            return;
        _velocity.y = _currentCharacterConfig.JumpPower;
        OnJumping?.Invoke();
    }

    private void Gravity(float time)
    {
        if (_characterController.isGrounded && _velocity.y < 0) 
            _velocity.y = -1;

        _velocity.y -= _currentCharacterConfig.GravityValue * time;

        if(!_characterController.isGrounded && _characterController.velocity.y < -1) 
            _isFalling = true;
        else 
            _isFalling = false;

        if (_isFalling) 
            _currentSpeed = Mathf.Lerp(_currentSpeed, _currentCharacterConfig.InFlySpeed, _currentCharacterConfig.FallingValue * time);
    } 

    private void Landing()
    {
        Physics.queriesHitTriggers = false;

        RaycastHit hit;
        var positionRay = _characterTransform.position;
        var currentHeight = 0f;

        positionRay.y -= _characterHeight / 2;

        if (Physics.Raycast(positionRay, _characterTransform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            currentHeight = Mathf.Abs(hit.point.y - positionRay.y);

            if (!_characterController.isGrounded && _dropHeight < currentHeight)
                _dropHeight = currentHeight;

            if (_backHeight > 0.1f && currentHeight < 0.1f)
            {
                OnLanding?.Invoke(_dropHeight);
                _dropHeight = 0;
            }
                
            _backHeight = currentHeight;
        }
    }

    private void Subscribes()
    {
        _characterMovementsInput.OnChangeMovementInput += Walking;
        _characterMovementsInput.OnChangeRuningInput += Runing;
        _characterMovementsInput.OnChangeCrouchingInput += Crouching;
        _characterMovementsInput.OnJumpingInput += Jumping;
    }

    private void Unsubscribes()
    {
        _characterMovementsInput.OnChangeMovementInput -= Walking;
        _characterMovementsInput.OnChangeRuningInput -= Runing;
        _characterMovementsInput.OnChangeCrouchingInput += Crouching;
        _characterMovementsInput.OnJumpingInput -= Jumping;
    }
}
