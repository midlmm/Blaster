using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraRotateInput
{
    public event Action<Vector2> OnChangeMouseInput;

    public virtual void Tick() { }
}
