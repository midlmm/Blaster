using System;
using System.Collections;
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
    private PlayerAnimatorController _playerAnimatorController;

    public void Initialize(IToolitemInput toolitemInput, Transform transformCamera, PlayerAnimatorController playerAnimatorController)
    {
        _toolitemInput = toolitemInput;
        _transformCamera = transformCamera;
        _playerAnimatorController = playerAnimatorController;

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

        _toolitemInput.SetDelayUse(_currentDelayUsing);

        _currentToolitem?.Put();
        _playerAnimatorController.SetAnimatorOverride(toolitemConfig.AnimatorOverride);

        if (!_toolitems.ContainsKey(toolitemConfig.Prefab.name))
        {
            var toolitem = Instantiate(toolitemConfig.Prefab, _armPoint).GetComponent<IToolitemable>();
            toolitem.Initialize(_transformCamera);

            _toolitems.Add(toolitemConfig.Prefab.name, toolitem);
        }

        _currentToolitem = _toolitems[toolitemConfig.Prefab.name];

        OnChangeToolitem?.Invoke(_currentToolitem);

        _currentToolitem.Took();
        _playerAnimatorController.OnEquip();
    }

    private void Use()
    {
        if (_currentToolitem.Use())
            _playerAnimatorController.OnUse();
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
        if(_currentToolitem.Recharge())
            _playerAnimatorController.OnRecharge();
    }

    private void ProcessingUsing()
    {
        if (!_isUsing)
            return;

        if (_timeLeftUsing <= 0)
        {
            if(_currentToolitem.Using())
                _playerAnimatorController.OnUse();

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
