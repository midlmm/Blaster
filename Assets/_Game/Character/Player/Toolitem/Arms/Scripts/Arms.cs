using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour, IToolitemable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    public void Initialize(Transform transformCamera)
    {
        
    }

    public void Took()
    {
        gameObject.SetActive(true);
    }

    public void Put()
    {
        gameObject.SetActive(false);
    }

    public bool Use() => true;
    public virtual bool Using() => false;
    public virtual void AlternativeUsing(bool isActive) { }
    public bool Recharge() => true;
}
