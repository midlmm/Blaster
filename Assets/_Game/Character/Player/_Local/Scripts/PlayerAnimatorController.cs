using UnityEngine;

public class PlayerAnimatorController
{
    private Animator _animator;

    public PlayerAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void OnEquip()
    {
        _animator.SetTrigger("OnEquip");
    }

    public void OnUse()
    {
        _animator.SetTrigger("OnUse");
    }

    public void OnRecharge()
    {
        _animator.SetTrigger("OnRecharge");
    }

    public void SetAnimatorOverride(AnimatorOverrideController animatorOverride)
    {
        _animator.runtimeAnimatorController = animatorOverride;
    }
}
