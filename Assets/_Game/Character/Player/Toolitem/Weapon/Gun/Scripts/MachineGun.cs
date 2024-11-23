using UnityEngine;
using Zenject;

public class MachineGun : BaseGun
{
    public override void Using()
    {
        float a = Random.Range(-_configData.Spread, _configData.Spread);
        float b = Random.Range(-_configData.Spread, _configData.Spread);

        if (!ProcessingShoot()) return;

        Shoot(new Vector3(_transformCamera.forward.x + a, _transformCamera.forward.y + b, _transformCamera.forward.z));
    }
}
