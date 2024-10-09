using UnityEngine;

public class ShotgunWeapon : BaseWeapon
{
    [Header("Local")]
    [SerializeField] private int _countPatron;

    public override void Shoot()
    {
        if (!ProcessingShoot()) return;

        for (int i = 0; i < _countPatron; i++)
        {
            float a = Random.Range(0, _spread);
            SpawnBullet(new Vector3(_firePoint.right.x - a, _firePoint.right.y - a, _firePoint.right.z));
        }
    }

}
