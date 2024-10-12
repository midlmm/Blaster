using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Gun Config")]
public class GunConfigData : ScriptableObject
{
    public float Spread => _spread;
    public int Damage => _damage;
    public int MaxAmmo => _maxAmmo;
    public float TimeRecharge => _timeRecharge;

    [SerializeField] private float _spread;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeRecharge;
}
