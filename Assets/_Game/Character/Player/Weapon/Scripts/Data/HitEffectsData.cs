using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Hit Effects")]
public class HitEffectsData : ScriptableObject
{
    public GameObject[] Effects => _effects;

    [SerializeField] private GameObject[] _effects;
}
