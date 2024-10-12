using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider : MonoBehaviour, IDamageable
{
    [SerializeField] private ETypeHit _typeHit;
    [SerializeField] private HitEffectsData _hitEffectsData;

    public void TakeDamage(DamageInfo damageInfo)
    {
        var effect = _hitEffectsData.HitConfigs[0].Effect;

        foreach (var item in _hitEffectsData.HitConfigs)
            if(item.TypeHit == _typeHit) 
                effect = item.Effect;

        Instantiate(effect, damageInfo.Point, Quaternion.LookRotation(damageInfo.Normal), transform);
    }
}
