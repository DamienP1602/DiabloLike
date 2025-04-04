using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
