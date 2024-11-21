using UnityEngine;

public class PlayerAnimatorController
{
    private Animator _animator;

    public PlayerAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void Use()
    {
        _animator.SetTrigger("OnUse");
    }

    public void Equip()
    {
        _animator.SetTrigger("OnEquip");
    }

    public void Recharge()
    {
        _animator.SetTrigger("OnRecharge");
    }

    public void SetRuntimeAnimatorController(AnimatorOverrideController animatorOverride)
    {
        _animator.runtimeAnimatorController = animatorOverride;
    }
}
