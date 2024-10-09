using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Toolitem Config")]
public class ToolitemConfigData : ScriptableObject
{
    public Vector3 OffsetPosition => _offsetPosition;
    public Vector3 OffsetRotation => _offsetRotation;
    public GameObject Prefab => _prefab;

    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _offsetRotation;
    [SerializeField] private GameObject _prefab;
}
