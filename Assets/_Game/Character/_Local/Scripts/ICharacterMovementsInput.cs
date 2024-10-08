using System;
using UnityEngine;

public interface ICharacterMovementsInput 
{
    public event Action<Vector2> OnChangeMovementInput;
    public event Action<bool> OnChangeRuningInput;
    public event Action<bool> OnChangeCrouchingInput;
    public event Action OnJumpingInput;

    public virtual void Tick() {}
}
