using UnityEngine;

public class GunAnimatorController
{
    private Animator _animator;

    public GunAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void OnRecharge()
    {
        _animator.SetTrigger("OnRecharge");
    }
}
