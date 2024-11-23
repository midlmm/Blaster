using System;

public interface IToolitemable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    public abstract void Took();
    public abstract void Put();

    public virtual void Use() { }
    public virtual void Using() { }
    public virtual void AlternativeUsing() { }
    public virtual void Recharge() { }
}
