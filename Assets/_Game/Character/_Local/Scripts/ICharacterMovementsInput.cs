using System;
using UnityEngine;

public interface ICharacterMovementsInput : ITickable
{
    public event Action<Vector2> OnChangeMovementInput;
    public event Action<bool> OnChangeRuningInput;
    public event Action<bool> OnChangeCrouchingInput;
    public event Action OnJumpingInput;
}
