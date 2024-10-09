using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Character Config")]
public class CharacterConfigData : ScriptableObject
{
    public float OnGroundSpeed => _onGroundSpeed;
    public float OnAccelerate => _onAccelerate;
    public float OnDecelerate => _onDecelerate;
    public float InFlySpeed => _inFlySpeed;
    public float GravityValue => _gravityValue;
    public float JumpPower => _jumpPower;
    public float FallingValue => _fallingValue;
    public float CrouchHeight => _crouchHeight;

    [SerializeField] private float _onGroundSpeed;
    [SerializeField] private float _onAccelerate;
    [SerializeField] private float _onDecelerate;
    [SerializeField] private float _inFlySpeed;
    [Space]
    [SerializeField] private float _gravityValue;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _fallingValue;
    [SerializeField] private float _crouchHeight;
}
