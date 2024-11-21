using System.Collections;
using UnityEngine;

public class SingleGun : BaseGun
{
    public override void Use()
    {
        if (!ProcessingShoot())
            return;

        float a1 = Random.Range(-_configData.Spread, _configData.Spread);
        SpawnBullet(new Vector3(_firePoint.right.x + a1, _firePoint.right.y + a1, _firePoint.right.z));
    }
}
