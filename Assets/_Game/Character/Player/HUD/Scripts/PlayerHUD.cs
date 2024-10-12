using UnityEngine;
using Zenject;

public class PlayerHUD : MonoBehaviour
{
    public RechargeView RechargeView => _rechargeView;

    [SerializeField] private RechargeView _rechargeView;

    private Player _player;

    [Inject]
    private void Initialize(Player player)
    {
        _player = player;

        _player.Toolitem.OnChangeToolitem += ChangeToolitem;
    }

    private void ChangeToolitem(IToolitemable toolitem)
    {
        _rechargeView.OnChangeToolitem(toolitem);
    }

    private void OnDestroy()
    {
        _player.Toolitem.OnChangeToolitem -= ChangeToolitem;
    }
}
