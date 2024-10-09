using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public RechargeView RechargeView => _rechargeView;

    [SerializeField] private RechargeView _rechargeView;
}
