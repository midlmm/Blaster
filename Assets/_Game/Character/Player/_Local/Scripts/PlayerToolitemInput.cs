using System;
using UnityEngine;

public class PlayerToolitemInput : IToolitemInput
{
    public event Action OnShootInput;
    public event Action<bool> OnChangeShootingInput;
    public event Action<bool> OnChangeZoomInput;
    public event Action OnRechargeInput;
    public event Action<int> OnChangeToolitemInput;

    private bool _currentShootingInput;
    private bool _currentZoomInput;

    public void Tick()
    {
        ShootInput();
        ShootingInput();
        ZoomInput();
        RechargeInput();
        ChangeToolitemInput();
    }

    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0)) OnShootInput?.Invoke();
    }

    public void ShootingInput()
    {
        var isShootingInput = false;

        if (Input.GetMouseButton(0)) isShootingInput = true;

        if (_currentShootingInput != isShootingInput)
            OnChangeShootingInput?.Invoke(isShootingInput);

        _currentShootingInput = isShootingInput;
    }

    public void ZoomInput()
    {
        var isZoomInput = false;

        if (Input.GetMouseButtonDown(1)) isZoomInput = true;

        if (_currentZoomInput != isZoomInput)
            OnChangeZoomInput?.Invoke(isZoomInput);

        _currentZoomInput = isZoomInput;
    }

    private void RechargeInput()
    {
        if (Input.GetKeyDown(KeyCode.R)) OnRechargeInput?.Invoke();
    }

    public void ChangeToolitemInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnChangeToolitemInput?.Invoke(0);
        else  if (Input.GetKeyDown(KeyCode.Alpha2)) OnChangeToolitemInput?.Invoke(1);
        else  if (Input.GetKeyDown(KeyCode.Alpha3)) OnChangeToolitemInput?.Invoke(2);
    }
}
