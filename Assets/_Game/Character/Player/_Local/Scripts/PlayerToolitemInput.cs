using System;
using UnityEngine;

public class PlayerToolitemInput : IToolitemInput
{
    public event Action OnUseInput;
    public event Action<bool> OnChangeUseInput;
    public event Action<bool> OnChangeAlternativeUseInput;
    public event Action OnRechargeInput;
    public event Action<int> OnChangeToolitemInput;

    private bool _currentUsingInput;
    private bool _currentAternativeUsingInput;

    private float _currentDelayUsing;
    private float _timeLeftUsing;
    private bool _isUse;

    public void Tick()
    {
        UseInput();
        UsingInput();
        AternativeUsingInput();
        RechargeInput();
        ChangeToolitemInput();
    }

    private void UseInput()
    {
        ProccesingUse();

        if (Input.GetMouseButtonDown(0) && !_isUse)
        {
            OnUseInput?.Invoke();
            _isUse = true;
        }
    }

    private void ProccesingUse()
    {
        if (!_isUse)
            return;

        if (_timeLeftUsing <= 0)
        {
            _isUse = false;

            _timeLeftUsing = _currentDelayUsing;
        }
        else
        {
            _timeLeftUsing -= Time.deltaTime;
        }
    }

    public void UsingInput()
    {
        var isUsingInput = false;

        if (Input.GetMouseButton(0))
            isUsingInput = true;

        if (_currentUsingInput != isUsingInput)
            OnChangeUseInput?.Invoke(isUsingInput);

        _currentUsingInput = isUsingInput;
    }

    public void AternativeUsingInput()
    {
        var isAternativeInputInput = false;

        if (Input.GetMouseButton(1))
            isAternativeInputInput = true;

        if (_currentAternativeUsingInput != isAternativeInputInput)
            OnChangeAlternativeUseInput?.Invoke(isAternativeInputInput);

        _currentAternativeUsingInput = isAternativeInputInput;
    }

    private void RechargeInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnRechargeInput?.Invoke();
    }

    public void ChangeToolitemInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnChangeToolitemInput?.Invoke(0);
        else  if (Input.GetKeyDown(KeyCode.Alpha2))
            OnChangeToolitemInput?.Invoke(1);
        else  if (Input.GetKeyDown(KeyCode.Alpha3))
            OnChangeToolitemInput?.Invoke(2);
    }

    public void SetDelayUse(float delay)
    {
        _currentDelayUsing = delay;
    }
}
