using System;
using UnityEngine;

public interface IToolitemable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    public abstract void Initialize(Transform transformCamera);

    public abstract void Took();
    public abstract void Put();

    public virtual bool Use() => false;
    public virtual bool Using() => false;
    public virtual void AlternativeUsing(bool isActive) { }
    public virtual bool Recharge() => false;
}
