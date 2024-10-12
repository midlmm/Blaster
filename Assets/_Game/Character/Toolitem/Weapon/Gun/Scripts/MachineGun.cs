using UnityEngine;
using Zenject;

public class MachineGun : BaseGun
{
    [Header("Local")]
    [SerializeField] private float _delay;

    private float _currentDelay;

    public override void Using()
    {
        float a = Random.Range(-_configData.Spread, _configData.Spread);
        float b = Random.Range(-_configData.Spread, _configData.Spread);

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
