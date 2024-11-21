using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Player Config")]
public class PlayerConfigData : ScriptableObject
{
    public float ShakingSpeedRatios => _shakingSpeedRatios;
    public float AmountSwaying => _amountSwaying;
    public float ForveLanding => _forceLanding;
    public float Sensitivity => _sensitivity;
    public float MaxAngleY => _maxAngleY;

    [SerializeField] private float _shakingSpeedRatios;
    [SerializeField] private float _amountSwaying;
    [SerializeField] private float _forceLanding;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxAngleY;
}
