using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IToolitemable
{
    [SerializeField] private protected float _spread;
    [SerializeField] private protected int _damage;
    [SerializeField] private protected int _maxAmmo;
    [SerializeField] private protected float _timeRecharge;

    [SerializeField] private protected Transform _firePoint;
    [SerializeField] private ParticleSystem _fireEffect;
    
    private RechargeView _rechargeView;
    private HitEffectsData _hitEffects;

    private protected int _currentAmmo;

    public void Initialize(PlayerHUD playerHUD)
    {
        _rechargeView = playerHUD.RechargeView;

        _currentAmmo = _maxAmmo;
        _rechargeView.DisplayCountAmmo(_currentAmmo);

        _hitEffects = Resources.Load<HitEffectsData>("HitEffectsData");
    }

    private void Update()
    {
        TimerUpdate();
    }

    public void Recharge()
    {
        if (_currentAmmo >= _maxAmmo)
            return;
        _rechargeView.DispaySetActiveSlider(true, _timeRecharge);
        TimerStart(_timeRecharge);
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

        if (hitInfo.transform.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(_damage);

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

    public virtual void Shoot() { }
    public virtual void Shooting() { }
    public virtual void Zoom() { }

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
