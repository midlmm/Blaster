using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Toolitem : MonoBehaviour
{
    public event Action<IToolitemable> OnChangeToolitem;

    [SerializeField] private Transform _armPoint;
    [SerializeField] private ToolitemConfigData[] _toolitemConfigDatas;

    private Dictionary<string, IToolitemable> _toolitems;

    private IToolitemable _currentToolitem;

    private bool _isUsing;
    private bool _isAlternativeUsing;

    private IToolitemInput _toolitemInput;
    private PlayerAnimatorController _playerAnimatorController;

    public void Initialize(IToolitemInput toolitemInput, PlayerAnimatorController playerAnimatorController)
    {
        _toolitemInput = toolitemInput;
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

        if (_isUsing)
            _currentToolitem.Using();
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

        _playerAnimatorController.SetRuntimeAnimatorController(toolitemConfig.AnimatorOverride);

        _currentToolitem.Took(_playerAnimatorController);
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
