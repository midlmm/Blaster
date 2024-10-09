using System;

public interface IToolitemInput 
{
    public event Action OnShootInput;
    public event Action<bool> OnChangeShootingInput;
    public event Action<bool> OnChangeZoomInput;
    public event Action OnRechargeInput;
    public event Action<int> OnChangeToolitemInput;

    public virtual void Tick() { }
}
