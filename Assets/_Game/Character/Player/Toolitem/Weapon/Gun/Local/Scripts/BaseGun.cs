using System;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeaponable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    [SerializeField] private protected GunConfigData _configData;

    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private Animator _animator;

    private protected int _currentAmmo;
    private protected Transform _transformCamera;

    private CameraHolder _cameraHolder;

    private GunAnimatorController _animatorController;
    private Timer _timerRecharge;
    private Timer _timerEquip;

    private Transform _transformArmsHolder;

    private bool _isLock;
    private bool _isRecharge;
    private bool _isZooming;

    public void Initialize(Transform transformCamera)
    {
        _transformCamera = transformCamera;
    }

    private void Awake()
    {
        _timerEquip = new Timer(_configData.TimeEquip, () => {
            _isLock = false;
        });
    }

    public void Start()
    {
        _currentAmmo = _configData.MaxAmmo;
        OnChangeAmmo?.Invoke(_currentAmmo);

        _animatorController = new GunAnimatorController(_animator);

        _timerRecharge = new Timer(_configData.TimeRecharge, OnChangeTimeRecharge, RechargeTimerEnd);

        _transformArmsHolder = transform.parent.parent.parent.parent.parent;

        _cameraHolder = _transformCamera.GetComponent<CameraHolder>();
    }

    private void Update()
    {
        _timerRecharge.Tick();
        _timerEquip.Tick();

        Zoom(); 
    }

    public bool Recharge()
    {
        if (_currentAmmo >= _configData.MaxAmmo || _isLock || _isRecharge)
            return false;

        OnChangeStateRecharge?.Invoke(true, _configData.TimeRecharge);

        _isRecharge = true;
        _timerRecharge.TimerStart();

        _animatorController.OnRecharge();

        return true;
    }

    public void AlternativeUsing(bool isActive)
    {
        _isZooming = isActive;
    }

    public void Took()
    {
        gameObject.SetActive(true);
        OnChangeAmmo?.Invoke(_currentAmmo);

        _isLock = true;
        _timerEquip.TimerStart();
    }

    public void Put()
    {
        gameObject.SetActive(false);

        _transformArmsHolder.localPosition = Vector3.zero;
        _transformArmsHolder.localRotation = Quaternion.Euler(Vector3.zero);
        _cameraHolder.SetFov(_cameraHolder.DefaultFov);
    }

    private protected void Shoot(Vector3 direction)
    {
        var isHit = Physics.Raycast(_transformCamera.position, direction, out var hitInfo);

        _fireEffect.Play();

        if (isHit)
            Hit(hitInfo);
    }

    private void Zoom()
    {
        var targetPosition = Vector3.zero;
        var targetRotation = Vector3.zero;
        var targetFov = _cameraHolder.DefaultFov;

        if(_isZooming)
        {
            targetPosition = _configData.PositionZoom;
            targetRotation = _configData.RotationZoom;
            targetFov = _configData.FovZoom;
        }

        _transformArmsHolder.localPosition = Vector3.Lerp(_transformArmsHolder.localPosition, targetPosition, _configData.SpeedZoom * Time.deltaTime);
        _transformArmsHolder.localRotation = Quaternion.Euler(Vector3.Lerp(_transformArmsHolder.localRotation.eulerAngles, targetRotation, _configData.SpeedZoom * Time.deltaTime));
        _cameraHolder.SetFov(Mathf.Lerp(_cameraHolder.Camera.fieldOfView, targetFov, _configData.SpeedZoom * Time.deltaTime));
    }

    private void Hit(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(new DamageInfo(_configData.Damage, hitInfo.normal, hitInfo.point));
    }

    private protected bool ProcessingShoot()
    {
        if (_currentAmmo <= 0 || _isLock || _isRecharge) 
            return false;

        _currentAmmo--;

        OnChangeAmmo?.Invoke(_currentAmmo);

        return true;
    }

    public virtual bool Use() => false;
    public virtual bool Using() => false;

    private void RechargeTimerEnd()
    {
        _currentAmmo = _configData.MaxAmmo;

        OnChangeStateRecharge?.Invoke(false, _configData.TimeRecharge);
        OnChangeAmmo?.Invoke(_currentAmmo);

        _isRecharge = false;
    }
}
