using System;
using UnityEngine;

public class PlayerMovementsInput : ICharacterMovementsInput
{
    public event Action<Vector2> OnChangeMovementInput;
    public event Action<bool> OnChangeRuningInput;
    public event Action<bool> OnChangeCrouchingInput;
    public event Action OnJumpingInput;

    private Vector2 _currentDirectionInput;
    private bool _currentRuningInput;
    private bool _currentCrouchingInput;

    public void Tick()
    {
        MovementInput();
        RuningInput();
        CrouchInput();
        JumpInput();
    }

    private void JumpInput()
    {
        if (Input.GetKey(KeyCode.Space)) OnJumpingInput?.Invoke();
    }

    public void MovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        var directionInput = new Vector2(x, y);

        if(_currentDirectionInput != directionInput)
            OnChangeMovementInput?.Invoke(directionInput);

        _currentDirectionInput = directionInput;
    }

    public void RuningInput()
    {
        var isRunningInput = false;

        if (Input.GetKey(KeyCode.LeftShift)) isRunningInput = true;

        if (_currentRuningInput != isRunningInput)
            OnChangeRuningInput?.Invoke(isRunningInput);

        _currentRuningInput = isRunningInput;
    }

    public void CrouchInput()
    {
        var isCrouchingInput = false;

        if (Input.GetKey(KeyCode.LeftControl)) isCrouchingInput = true;

        if (_currentCrouchingInput != isCrouchingInput)
            OnChangeCrouchingInput?.Invoke(isCrouchingInput);

        _currentCrouchingInput = isCrouchingInput;
    }
}
