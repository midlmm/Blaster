using UnityEngine;

public class ShotGun : BaseGun
{
    [Header("Local")]
    [SerializeField] private int _countBullets;

    public override void Use()
    {
        if (!ProcessingShoot()) return;

        for (int i = 0; i < _countBullets; i++)
        {
            float a = Random.Range(-_configData.Spread, _configData.Spread);
            float b = Random.Range(-_configData.Spread, _configData.Spread);

            Shoot(new Vector3(_transformCamera.forward.x + a, _transformCamera.forward.y + b, _transformCamera.forward.z));
        }
    }

}
