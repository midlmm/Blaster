using System;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeaponable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    [SerializeField] private ParticleSystem _fireEffect;

    [SerializeField] private protected GunConfigData _configData;

    private protected int _currentAmmo;
    private protected Transform _transformCamera;

    private Timer _timerRecharge;

    public void Start()
    {
        _currentAmmo = _configData.MaxAmmo;
        OnChangeAmmo?.Invoke(_currentAmmo);

        _timerRecharge = new Timer(_configData.TimeRecharge, OnChangeTimeRecharge, TimerEnd);
    }

    private void Update()
    {
        _timerRecharge.Tick();
    }

    public void Recharge()
    {
        if (_currentAmmo >= _configData.MaxAmmo)
            return;

        OnChangeStateRecharge?.Invoke(true, _configData.TimeRecharge);

        _timerRecharge.TimerStart();
    }

    private protected void Shoot(Vector3 direction)
    {
        var isHit = Physics.Raycast(_transformCamera.position, direction, out var hitInfo);

        _fireEffect.Play();

        if (isHit)
            Hit(hitInfo);
    }

    private void Hit(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(new DamageInfo(_configData.Damage, hitInfo.normal, hitInfo.point));
    }

    private protected bool ProcessingShoot()
    {
        if (_currentAmmo <= 0) 
            return false;

        _currentAmmo--;

        OnChangeAmmo?.Invoke(_currentAmmo);

        return true;
    }

    public virtual void Use() { }
    public virtual void Using() { }
    public virtual void AlternativeUsing(bool isActive) { }

    public void Took(Transform transformCamera)
    {
        _transformCamera = transformCamera;

        gameObject.SetActive(true);
        OnChangeAmmo?.Invoke(_currentAmmo);
    }

    public void Put()
    {
        gameObject.SetActive(false);
    }

    private void TimerEnd()
    {
        _currentAmmo = _configData.MaxAmmo;

        OnChangeStateRecharge?.Invoke(false, _configData.TimeRecharge);
        OnChangeAmmo?.Invoke(_currentAmmo);
    }
}
