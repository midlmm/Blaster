using System;
using System.Collections.Generic;
using UnityEngine;

public class Toolitem : MonoBehaviour
{
    public event Action<IToolitemable> OnChangeToolitem;

    [SerializeField] private Transform _armPoint;
    [SerializeField] private ToolitemConfigData[] _toolitemConfigDatas;

    private Dictionary<string, IToolitemable> _toolitems;

    private IToolitemable _currentToolitem;

    private bool _isUsing;
    private float _timeLeftUsing;
    private float _currentDelayUsing;

    private IToolitemInput _toolitemInput;
    private Transform _transformCamera;

    public void Initialize(IToolitemInput toolitemInput, Transform transformCamera)
    {
        _toolitemInput = toolitemInput;
        _transformCamera = transformCamera;

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

        ProcessingUsing();
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
        _currentDelayUsing = toolitemConfig.DelayUsing;

        _currentToolitem?.Put();

        if (!_toolitems.ContainsKey(toolitemConfig.Prefab.name))
        {
            _toolitems.Add(toolitemConfig.Prefab.name, Instantiate(toolitemConfig.Prefab, _armPoint).GetComponent<IToolitemable>());
        }

        _currentToolitem = _toolitems[toolitemConfig.Prefab.name];

        OnChangeToolitem?.Invoke(_currentToolitem);

        _currentToolitem.Took(_transformCamera);
    }

    private void Use()
    {
        _currentToolitem.Use();
    }

    private void Using(bool isActive)
    {
        _isUsing = isActive;
    }

    private void AlternativeUsing(bool isActive)
    {
        _currentToolitem.AlternativeUsing(isActive);
    }

    private void Recharge()
    {
        _currentToolitem.Recharge();
    }

    private void ProcessingUsing()
    {
        if (!_isUsing)
            return;

        if (_timeLeftUsing <= 0)
        {
            _currentToolitem.Using();

            _timeLeftUsing = _currentDelayUsing;
        }
        else
        {
            _timeLeftUsing -= Time.deltaTime;
        }
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
