using System;
using UnityEngine;

public interface ICameraRotateInput
{
    public event Action<Vector2> OnChangeMouseInput;

    public virtual void Tick() { }
}
