using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolitem : MonoBehaviour
{
    [SerializeField] private Transform _armPoint;
    [SerializeField] private ToolitemConfigData[] _toolitemConfigDatas;

    private PlayerHUD _playerHUD;
    private IToolitemInput _toolitemInput;

    private IToolitemable _currentToolitem;

    private bool _isShooting;
    private bool _isZoom;

    public void Initialize(IToolitemInput toolitemInput, PlayerHUD playerHUD)
    {
        _toolitemInput = toolitemInput;
        _playerHUD = playerHUD;

        ChangeToolitem(0);

        Subscribe();
    }

    private void Update()
    {
        _toolitemInput.Tick();

        if (_isShooting)
            _currentToolitem.Shooting();

        if(_isZoom)
            _currentToolitem.Zoom();
    }

    private void ChangeToolitem(int key)
    {
        if (_toolitemConfigDatas.Length < key)
            return;

        if(_currentToolitem != null)
            _currentToolitem.Destroy();

        var toolitemConfig = _toolitemConfigDatas[key];

        var toolitem = Instantiate(toolitemConfig.Prefab, _armPoint).transform;

        toolitem.localPosition = toolitemConfig.OffsetPosition;
        toolitem.localRotation = Quaternion.Euler(toolitemConfig.OffsetRotation);

        _currentToolitem = toolitem.GetComponent<IToolitemable>();

        _currentToolitem.Initialize(_playerHUD);
    }

    private void Shoot()
    {
        _currentToolitem.Shoot();
    }

    private void Shooting(bool isActive)
    {
        _isShooting = isActive;
    }

    private void Zoom(bool isActive)
    {
        _isZoom = isActive;
    }

    private void Recharge()
    {
        _currentToolitem.Recharge();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _toolitemInput.OnShootInput += Shoot;
        _toolitemInput.OnChangeShootingInput += Shooting;
        _toolitemInput.OnChangeZoomInput += Zoom;
        _toolitemInput.OnRechargeInput += Recharge;
        _toolitemInput.OnChangeToolitemInput += ChangeToolitem;
    }

    private void Unsubscribe()
    {
        _toolitemInput.OnShootInput -= Shoot;
        _toolitemInput.OnChangeShootingInput -= Shooting;
        _toolitemInput.OnChangeZoomInput -= Zoom;
        _toolitemInput.OnRechargeInput -= Recharge;
        _toolitemInput.OnChangeToolitemInput -= ChangeToolitem;
    }
}
