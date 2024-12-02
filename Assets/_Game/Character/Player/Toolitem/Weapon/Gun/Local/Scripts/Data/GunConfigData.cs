using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Gun Config")]
public class GunConfigData : ScriptableObject
{
    public float Spread => _spread;
    public int Damage => _damage;
    public int MaxAmmo => _maxAmmo;
    public float TimeRecharge => _timeRecharge;
    public float TimeEquip => _timeEquip;
    public Vector3 PositionZoom => _positionZoom;
    public Vector3 RotationZoom => _rotationZoom;
    public float SpeedZoom => _speedZoom;
    public float FovZoom => _fovZoom;

    [SerializeField] private float _spread;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeRecharge;
    [SerializeField] private float _timeEquip;

    [Header("Zoom")]
    [SerializeField] private Vector3 _positionZoom;
    [SerializeField] private Vector3 _rotationZoom;
    [SerializeField] private float _speedZoom;
    [SerializeField] private float _fovZoom = 60f;
}
