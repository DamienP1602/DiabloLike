using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimatorController(RuntimeAnimatorController _controller)
    {
        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = null;
        animator.runtimeAnimatorController = _controller;
    }

    public void StartMovementAnimation()
    {
        animator.SetBool("isRunning", true);
    }

    public void StopMovementAnimation()
    {
        animator.SetBool("isRunning", false);
    }

    public void StartAttackAnimation()
    {
        animator.SetBool("attack", true);
    }

    public void StopAttackAnimation()
    {
        animator.SetBool("attack", false);
    }

    public void StartSpellAnimation(string _spellName)
    {
        animator.SetBool(_spellName, true);
    }

    public void StopSpellAnimation(string _spellName)
    {
        animator.SetBool(_spellName, false);
    }
}
