using System;
using UnityEngine;

public interface IToolitemable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    public abstract void Took(PlayerAnimatorController playerAnimatorController);
    public abstract void Put();

    public virtual void Use() { }
    public virtual void Using() { }
    public virtual void AlternativeUsing(bool isAcive) { }
    public virtual void Recharge() { }
}
