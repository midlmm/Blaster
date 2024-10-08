using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healths : MonoBehaviour
{
    public Action OnDead;

    public int Health { get; private set; }

    [SerializeField] private int _maxHealth;

    private void Awake()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        Health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health < 0)
        {
            Health = 0;
            OnDead?.Invoke();
        }
    }
}
