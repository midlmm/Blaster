using UnityEngine;
using Zenject;

public class MachineWeapon : BaseWeapon
{
    [Header("Local")]
    [SerializeField] private float _delay;

    private float _currentDelay;

    public override void Shooting()
    {
        float a = Random.Range(-_spread, _spread);
        float b = Random.Range(-_spread, _spread);

        if (_currentDelay <= 0)
        {
            if (!ProcessingShoot()) return;

            SpawnBullet(new Vector3(_firePoint.right.x + a, _firePoint.right.y + b, _firePoint.right.z));
            _currentDelay = _delay;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
        }
    }
}
