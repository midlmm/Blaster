using UnityEngine;

public class Player : MonoBehaviour, IDamageable 
{
    public CharacterMovements Movements { get; private set; }
    public CameraRotate CameraRotate { get; private set; }
    public Healths Healths { get; private set; }
    public Toolitem Toolitem => _toolitem;

    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Toolitem _toolitem;

    private ICharacterMovementsInput _movementsInput;
    private ICameraRotateInput _cameraRotateInput;
    private IToolitemInput _toolitemInput;

    public void Initialize()
    {
        _movementsInput = new PlayerMovementsInput();
        Movements = new CharacterMovements(_movementsInput, _characterTransform);

        _cameraRotateInput = new CameraRotateInput();
        CameraRotate = new CameraRotate(_cameraRotateInput, _characterTransform, _cameraTransform);

        _toolitemInput = new PlayerToolitemInput();
        _toolitem.Initialize(_toolitemInput);

        Healths = new Healths();

        Healths.OnDead += Dead;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movements.Tick(Time.deltaTime);
        CameraRotate.Tick();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        Healths.TakeDamage(damageInfo.Damage);
    }

    private void Dead()
    {
        Movements.OnDestroy();
        CameraRotate.OnDestroy();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Healths.OnDead -= Dead;
    }
}
