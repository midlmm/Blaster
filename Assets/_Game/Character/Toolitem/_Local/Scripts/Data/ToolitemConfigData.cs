using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Toolitem Config")]
public class ToolitemConfigData : ScriptableObject
{
    public GameObject Prefab => _prefab;

    [SerializeField] private GameObject _prefab;
}
