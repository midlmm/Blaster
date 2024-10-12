using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Datas/Hit Effects")]
public class HitEffectsData : ScriptableObject
{
    public List<HitConfig> HitConfigs => _hitConfigs;

    [SerializeField] private List<HitConfig> _hitConfigs;

    private void OnValidate()
    {
        var hitConfigs = _hitConfigs.ToArray();

        var effects = new List<GameObject>();

        for (int i = 0; i < Enum.GetNames(typeof(ETypeHit)).Length; i++)
        {
            if (hitConfigs.Length > i)
                effects.Add(hitConfigs[i].Effect);
            else
                effects.Add(null);
        }

        _hitConfigs.Clear();

        for (int i = 0; i < effects.Count; i++)
            _hitConfigs.Add(new HitConfig((ETypeHit)i, effects[i]));
    }
}

[Serializable]
public class HitConfig
{
    public ETypeHit TypeHit;
    public GameObject Effect;

    public HitConfig(ETypeHit typeHit, GameObject effect)
    {
        TypeHit = typeHit;
        Effect = effect;
    }
}
