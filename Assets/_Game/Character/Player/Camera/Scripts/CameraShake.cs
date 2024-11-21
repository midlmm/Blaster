using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraShake
{
    private const float DEFAULTIMESHAKE = 0.1f;

    private Vector3 _currentPosition;
    private float _distation;

    private Transform _cameraTransform;
    private PlayerConfigData _playerConfig;

    public CameraShake(Transform cameraTransform, PlayerConfigData playerConfig)
    {
        _cameraTransform = cameraTransform;
        _playerConfig = playerConfig;
    }

    public async void Shake(float force, float time = DEFAULTIMESHAKE)
    {
        Vector3 originalPos = _cameraTransform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < time)
        {
            float x = Random.Range(-force, force);
            float y = Random.Range(-force, force);
            float z = Random.Range(-force, force);

            _cameraTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

            elapsed += Time.deltaTime;

            await UniTask.NextFrame();
        }

        _cameraTransform.localPosition = originalPos;
    }

    public async void DownShake(float force, float time = DEFAULTIMESHAKE)
    {
        Vector3 originalPos = _cameraTransform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < time)
        {
            float y = Random.Range(-force, force);

            _cameraTransform.localPosition = new Vector3(originalPos.x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            await UniTask.NextFrame();
        }

        _cameraTransform.localPosition = originalPos;
    }

    public void Swaying(float speed)
    {
        _distation += (_cameraTransform.position - _currentPosition).magnitude * Time.deltaTime;
        _currentPosition = _cameraTransform.position;

        var rotation = new Vector3(_cameraTransform.localEulerAngles.x, _cameraTransform.localEulerAngles.y, Mathf.Sin(_distation * (speed * _playerConfig.ShakingSpeedRatios)) * _playerConfig.AmountSwaying);

        _cameraTransform.localEulerAngles = rotation;
    }
}
