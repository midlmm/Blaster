using System;
using UnityEngine;

public class PlayerToolitemInput : IToolitemInput
{
    public event Action OnUseInput;
    public event Action<bool> OnChangeUseInput;
    public event Action<bool> OnChangeAlternativeUseInput;
    public event Action OnRechargeInput;
    public event Action<int> OnChangeToolitemInput;

    private bool _currentShootingInput;
    private bool _currentZoomInput;

    public void Tick()
    {
        UseInput();
        UsingInput();
        AlternativeUsingInput();
        RechargeInput();
        ChangeToolitemInput();
    }

    private void UseInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnUseInput?.Invoke();
    }

    public void UsingInput()
    {
        var isShootingInput = false;

        if (Input.GetMouseButton(0))
            isShootingInput = true;

        if (_currentShootingInput != isShootingInput)
            OnChangeUseInput?.Invoke(isShootingInput);

        _currentShootingInput = isShootingInput;
    }

    public void AlternativeUsingInput()
    {
        var isZoomInput = false;

        if (Input.GetMouseButton(1))
            isZoomInput = true;

        if (_currentZoomInput != isZoomInput)
            OnChangeAlternativeUseInput?.Invoke(isZoomInput);

        _currentZoomInput = isZoomInput;
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
}
