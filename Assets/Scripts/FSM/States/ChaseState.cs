using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    MovementComponent movementComponent;
    DetectionComponent detectionComponent;
    AttackComponent attackComponent;

    public override void Start(BrainComponent _owner)
    {
        if (isStarted) return;
        nextTransition = new Transition(new IdleState(), TranstionCondition);
        isStarted = true;

        movementComponent = _owner.GetComponent<MovementComponent>();
        detectionComponent = _owner.GetComponent<DetectionComponent>();
        attackComponent = _owner.GetComponent<AttackComponent>();

        attackComponent.SetTarget(detectionComponent.Target.gameObject);
        movementComponent.SetTarget(detectionComponent.Target.gameObject, attackComponent.Range);

        Debug.Log("Change State => ChaseState");
    }

    public override void Update(BrainComponent _owner)
    {
        // Nothing
    }

    public override void Exit(BrainComponent _owner)
    {
        attackComponent.SetTarget(null);
        movementComponent.SetTarget(null,0.0f);
    }


    public override bool TranstionCondition()
    {
        return !detectionComponent.IsClose();
    }
}
