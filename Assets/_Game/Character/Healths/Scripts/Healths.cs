using System;
using UnityEngine;

public class Healths
{
    public event Action OnDead;

    public int Health { get; private set; }

    [SerializeField] private int _maxHealth;

    public void SetMaxHealth()
    {
        Health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(damage < 0)
            Debug.LogException(new Exception("Negative damage"));

        Health -= damage;

        if(Health < 0)
        {
            Health = 0;
            OnDead?.Invoke();
        }
    }
}
