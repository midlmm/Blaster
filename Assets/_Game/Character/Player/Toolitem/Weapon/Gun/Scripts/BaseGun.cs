using System;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeaponable
{
    public event Action<int> OnChangeAmmo;
    public event Action<float> OnChangeTimeRecharge;
    public event Action<bool, float> OnChangeStateRecharge;

    [SerializeField] private protected GunConfigData _configData;

    [SerializeField] private protected Transform _firePoint;
    [SerializeField] private ParticleSystem _fireEffect;

    private protected int _currentAmmo;

    private bool _isRecharge;
    private bool _isLocked;

    private Timer _timerRecharge;
    private Timer _timerEquip;

    private PlayerAnimatorController _playerAnimatorController;

    public void Start()
    {
        _currentAmmo = _configData.MaxAmmo;
        OnChangeAmmo?.Invoke(_currentAmmo);

        _timerRecharge = new Timer(_configData.TimeRecharge, TimerEnd, OnChangeTimeRecharge);
    }

    private void Update()
    {
        _timerRecharge.Tick();
    }

    public void Recharge()
    {
        if (_currentAmmo >= _configData.MaxAmmo && _isRecharge)
            return;

        OnChangeStateRecharge?.Invoke(true, _configData.TimeRecharge);

        _timerRecharge.TimerStart();

        _playerAnimatorController.Recharge();

        _isRecharge = true;
    }

    private protected void SpawnBullet(Vector3 direction)
    {
        var isHit = Physics.Raycast(_firePoint.position, direction, out var hitInfo);

        _fireEffect.Play();

        if (isHit)
            Hit(hitInfo);

        _playerAnimatorController.Use();
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

        if (_isRecharge || _isLocked)
            return false;

        _currentAmmo--;

        OnChangeAmmo?.Invoke(_currentAmmo);

        return true;
    }

    public virtual void Use() { }
    public virtual void Using() { }
    public virtual void AlternativeUsing(bool isActive)
    {
        
    }

    public void Took(PlayerAnimatorController playerAnimatorController)
    {
        gameObject.SetActive(true);
        OnChangeAmmo?.Invoke(_currentAmmo);

        //_isLocked = true;

        _playerAnimatorController = playerAnimatorController;

        playerAnimatorController.Equip();
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

        _isRecharge = false;
    }
}
