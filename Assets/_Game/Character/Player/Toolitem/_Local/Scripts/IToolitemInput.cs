using System;

public interface IToolitemInput : ITickable
{
    public event Action OnUseInput;
    public event Action<bool> OnChangeUseInput;
    public event Action<bool> OnChangeAlternativeUseInput;
    public event Action OnRechargeInput;
    public event Action<int> OnChangeToolitemInput;

    public abstract void SetDelayUse(float delay);
}
