using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Player Config")]
public class PlayerConfigData : ScriptableObject
{
    public float Sensitivity => _sensitivity;
    public float MaxAngleY => _maxAngleY;

    public float DivisorShakeLanding => _divisorShakeLanding;
    public float ShakingTimeLanding => _shakingTimeLanding;

    public float SwayingSpeedMultiplier => _swayingSpeedMultiplier;
    public float ForceSwaying => _forceSwaying;

    [Header("Camera")]

    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxAngleY;

    [SerializeField] private float _divisorShakeLanding;
    [SerializeField] private float _shakingTimeLanding;

    [SerializeField] private float _swayingSpeedMultiplier;
    [SerializeField] private float _forceSwaying;
}
