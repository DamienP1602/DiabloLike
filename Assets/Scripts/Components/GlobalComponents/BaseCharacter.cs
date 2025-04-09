using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(StatsComponent), typeof(AnimationComponent), typeof(MovementComponent))]
[RequireComponent (typeof(AttackComponent))]
public abstract class BaseCharacter : NetworkBehaviour
{
    [SerializeField] protected string characterName;
    public string CharacterName => characterName;

    protected StatsComponent statsComponent;
    protected AnimationComponent animationComponent;
    protected MovementComponent movementComponent;
    protected AttackComponent attackComponent;

    public StatsComponent StatsComponent => statsComponent;
    public AnimationComponent AnimationComponent => animationComponent;
    public MovementComponent MovementComponent => movementComponent;
    public AttackComponent AttackComponent => attackComponent;

    protected virtual void Start()
    {
        Init();
        EventAssignation();
    }
    protected virtual void Init()
    {
        statsComponent = GetComponent<StatsComponent>();
        animationComponent = GetComponent<AnimationComponent>();
        movementComponent = GetComponent<MovementComponent>();
        attackComponent = GetComponent<AttackComponent>();
    }

    protected virtual void EventAssignation()
    {
        statsComponent.OnDeath += Despawn;
        attackComponent.onLaunchAttack += movementComponent.SetCantMove;
    }

    protected virtual void Despawn()
    {
        Debug.Log(CharacterName + " Killed !");
        Destroy(gameObject);
    }
}
