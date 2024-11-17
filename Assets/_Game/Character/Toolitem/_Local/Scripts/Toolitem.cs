using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Toolitem : MonoBehaviour
{
    public Action<IToolitemable> OnChangeToolitem;

    [SerializeField] private Transform _armPoint;
    [SerializeField] private ToolitemConfigData[] _toolitemConfigDatas;

    private Dictionary<string, IToolitemable> _toolitems;

    private IToolitemInput _toolitemInput;

    private IToolitemable _currentToolitem;

    private bool _isShooting;
    private bool _isZoom;

    public void Initialize(IToolitemInput toolitemInput)
    {
        _toolitemInput = toolitemInput;

        _toolitems = new Dictionary<string, IToolitemable>();

        Subscribe();
    }

    private void Start()
    {
        ChangeToolitem(0);
    }

    private void Update()
    {
        _toolitemInput.Tick();

        if (_isShooting)
            _currentToolitem.Using();

        if(_isZoom)
            _currentToolitem.AlternativeUsing();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void ChangeToolitem(int key)
    {
        if (_toolitemConfigDatas.Length < key)
            return;

        var toolitemConfig = _toolitemConfigDatas[key];

        _currentToolitem?.Put();

        if (!_toolitems.ContainsKey(toolitemConfig.Prefab.name))
        {
            _toolitems.Add(toolitemConfig.Prefab.name, Instantiate(toolitemConfig.Prefab, _armPoint).GetComponent<IToolitemable>());
        }

        _currentToolitem = _toolitems[toolitemConfig.Prefab.name];

        OnChangeToolitem?.Invoke(_currentToolitem);

        _currentToolitem.Took();
    }

    private void Use()
    {
        _currentToolitem.Use();
    }

    private void Using(bool isActive)
    {
        _isShooting = isActive;
    }

    private void AlternativeUsing(bool isActive)
    {
        _isZoom = isActive;
    }

    private void Recharge()
    {
        _currentToolitem.Recharge();
    }

    private void Subscribe()
    {
        _toolitemInput.OnUseInput += Use;
        _toolitemInput.OnChangeUseInput += Using;
        _toolitemInput.OnChangeAlternativeUseInput += AlternativeUsing;
        _toolitemInput.OnRechargeInput += Recharge;
        _toolitemInput.OnChangeToolitemInput += ChangeToolitem;
    }

    private void Unsubscribe()
    {
        _toolitemInput.OnUseInput -= Use;
        _toolitemInput.OnChangeUseInput -= Using;
        _toolitemInput.OnChangeAlternativeUseInput -= AlternativeUsing;
        _toolitemInput.OnRechargeInput -= Recharge;
        _toolitemInput.OnChangeToolitemInput -= ChangeToolitem;
    }
}
