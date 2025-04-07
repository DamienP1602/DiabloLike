using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class AnimationComponent : ClientNetworkAnimator
{
    //[SerializeField] Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
        OnIsServerAuthoritative();
    }

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

    public void StartMovementAnimation()
    {
        Animator.SetBool("isRunning", true);
    }

    public void StopMovementAnimation()
    {
        Animator.SetBool("isRunning", false);
    }

    public void StartAttackAnimation()
    {
        Animator.SetBool("attack", true);
    }

    public void StopAttackAnimation()
    {
        Animator.SetBool("attack", false);
    }

    public void StartSpellAnimation(string _spellName)
    {
        Animator.SetBool(_spellName, true);
    }

    public void StopSpellAnimation(string _spellName)
    {
        Animator.SetBool(_spellName, false);
    }
}
