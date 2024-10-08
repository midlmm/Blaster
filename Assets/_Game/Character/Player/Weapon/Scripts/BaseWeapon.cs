using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] private protected float _spread;
    [SerializeField] private protected int _damage;
    [SerializeField] private protected int _maxAmmo;
    [SerializeField] private protected float _timeRecharge;

    [SerializeField] private protected Transform _firePoint;
    [SerializeField] private protected ParticleSystem _fireEffect;
    [SerializeField] private protected RechargeView _rechargeView;

    private HitEffectsData _hitEffects;

    private protected int _currentAmmo;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
        _hitEffects = Resources.Load<HitEffectsData>("HitEffectsData");
        _rechargeView.DisplayCountAmmo(_currentAmmo);
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
        var hitEffect = _hitEffects.Effects[1];

        if (hitInfo.transform.TryGetComponent<Healths>(out var healths))
            healths.TakeDamage(_damage);

        switch (hitInfo.transform.tag)
        {
            case Constants.Tags.CHARACTER:
                hitEffect = _hitEffects.Effects[0];
                break;
            case Constants.Tags.STONE:
                hitEffect = _hitEffects.Effects[1];
                break;
            case Constants.Tags.WOOD:
                hitEffect = _hitEffects.Effects[2];
                break;
            case Constants.Tags.METAL:
                hitEffect = _hitEffects.Effects[3];
                break;
            case Constants.Tags.SAND:
                hitEffect = _hitEffects.Effects[4];
                break;
            default:
                hitEffect = _hitEffects.Effects[1];
                break;
        }

        Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
    }

    private protected bool ProcessingShoot()
    {
        if (_currentAmmo <= 0) 
            return false;
        _currentAmmo--;
        _rechargeView.DisplayCountAmmo(_currentAmmo);
        return true;
    }

    private protected void Recharge()
    {
        if (_currentAmmo >= _maxAmmo)
            return;
        _rechargeView.DispaySetActiveSlider(true, _timeRecharge);
        TimerStart(_timeRecharge);
    }

    //Временно!!!!!!
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Press();
        }
        if (Input.GetMouseButton(0))
        {
            PressDown();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Recharge();
        }
        TimerUpdate();
    }

    public abstract void Press();
    public abstract void PressDown();

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
                _currentAmmo = _maxAmmo;
                _rechargeView.DispaySetActiveSlider(false, _timeRecharge);
                _rechargeView.DisplayCountAmmo(_currentAmmo);
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
        _rechargeView.DispayValueSlider(_timeRecharge - _timeLeft);
    }

    #endregion
}
