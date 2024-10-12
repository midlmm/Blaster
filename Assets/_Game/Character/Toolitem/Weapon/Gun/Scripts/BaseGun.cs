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

    public void Start()
    {
        _currentAmmo = _configData.MaxAmmo;
        OnChangeAmmo?.Invoke(_currentAmmo);
    }

    private void Update()
    {
        TimerUpdate();
    }

    public void Recharge()
    {
        if (_currentAmmo >= _configData.MaxAmmo)
            return;

        OnChangeStateRecharge?.Invoke(true, _configData.TimeRecharge);

        TimerStart(_configData.TimeRecharge);
    }

    private protected void SpawnBullet(Vector3 direction)
    {
        var isHit = Physics.Raycast(_firePoint.position, direction, out var hitInfo);

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
    public virtual void AlternativeUsing() { }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region Timer

    private float _timeLeft = 0f;
    private bool _timerOn = false;

    public void TimerUpdate()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }
            else
            {
                _timerOn = false;
                _currentAmmo = _configData.MaxAmmo;

                OnChangeStateRecharge?.Invoke(false, _configData.TimeRecharge);
                OnChangeAmmo?.Invoke(_currentAmmo);
            }
        }
    }

    public void TimerStart(float time)
    {
        _timeLeft = time;
        _timerOn = true;
    }

    public void TimerStop()
    {
        _timerOn = false;
    }

    public void UpdateTimeText()
    {
        OnChangeTimeRecharge?.Invoke(_configData.TimeRecharge - _timeLeft);
    }

    #endregion
}
