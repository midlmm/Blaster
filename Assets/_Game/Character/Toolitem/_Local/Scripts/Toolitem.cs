using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Toolitem : MonoBehaviour
{
    public Action<IToolitemable> OnChangeToolitem;

    [SerializeField] private Transform _armPoint;
    [SerializeField] private ToolitemConfigData[] _toolitemConfigDatas;

    private List<GameObject> _toolitems;

    private IToolitemInput _toolitemInput;

    private IToolitemable _currentToolitem;

    private bool _isShooting;
    private bool _isZoom;

    public void Initialize(IToolitemInput toolitemInput)
    {
        _toolitemInput = toolitemInput;

        _toolitems = new List<GameObject>();

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

    private void ChangeToolitem(int key)
    {
        if (_toolitemConfigDatas.Length < key)
            return;

        var toolitemConfig = _toolitemConfigDatas[key];
        GameObject toolitem = null;

        foreach (var item in _toolitems)
        {
            item.SetActive(false);
            if (item.name == toolitemConfig.Prefab.name + "(Clone)")
                toolitem = item;
        }
            
        if (toolitem != null)
        {
            toolitem.SetActive(true);
        }
        else
        {
            toolitem = Instantiate(toolitemConfig.Prefab, _armPoint);

            toolitem.transform.localPosition = toolitemConfig.OffsetPosition;
            toolitem.transform.localRotation = Quaternion.Euler(toolitemConfig.OffsetRotation);

            _toolitems.Add(toolitem);
        }

        _currentToolitem = toolitem.GetComponent<IToolitemable>();

        OnChangeToolitem?.Invoke(_currentToolitem);
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

    private void OnDestroy()
    {
        Unsubscribe();
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
