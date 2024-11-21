using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IDamageable 
{
    public PlayerMovements Movements { get; private set; }
    public PlayerAnimatorController AnimatorController { get; private set; }
    public Healths Healths { get; private set; }
    public Toolitem Toolitem => _toolitem;

    [SerializeField] private PlayerConfigData _config;
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Toolitem _toolitem;

    private IToolitemInput _toolitemInput;

    [Inject]
    private void Initialize(ICharacterMovementsInput characterMovementsInput, ICameraRotateInput cameraRotateInput, IToolitemInput toolitemInput)
    {
        Movements = new PlayerMovements(characterMovementsInput, cameraRotateInput, _characterTransform, _cameraTransform, _config);
        AnimatorController = new PlayerAnimatorController(_animator);

        _toolitemInput = toolitemInput;
        _toolitem.Initialize(_toolitemInput, AnimatorController);

        Healths = new Healths();

        Healths.OnDead += Dead;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movements.Tick();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        Healths.TakeDamage(damageInfo.Damage);
    }

    private void Dead()
    {
        Movements.OnDestoy();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Healths.OnDead -= Dead;
    }
}
