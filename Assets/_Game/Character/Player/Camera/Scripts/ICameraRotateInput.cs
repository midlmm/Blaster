using System;
using UnityEngine;

public interface ICameraRotateInput : ITickable
{
    public event Action<Vector2> OnChangeMouseInput;
}
