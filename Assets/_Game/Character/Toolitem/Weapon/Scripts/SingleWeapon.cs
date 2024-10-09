using UnityEngine;

public class SingleWeapon : BaseWeapon
{
    public override void Shoot()
    {
        if (!ProcessingShoot()) return;

        float a1 = Random.Range(-_spread, _spread);
        SpawnBullet(new Vector3(_firePoint.right.x + a1, _firePoint.right.y + a1, _firePoint.right.z));
    }
}
