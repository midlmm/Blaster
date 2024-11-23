using System.Collections;
using UnityEngine;

public class SingleGun : BaseGun
{
    public override void Use()
    {
        if (!ProcessingShoot())
            return;

        float a = Random.Range(-_configData.Spread, _configData.Spread);
        float b = Random.Range(-_configData.Spread, _configData.Spread);

        Shoot(new Vector3(_transformCamera.forward.x + a, _transformCamera.forward.y + b, _transformCamera.forward.z));
    }
}