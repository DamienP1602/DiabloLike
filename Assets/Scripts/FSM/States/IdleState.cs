using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    DetectionComponent detection = null;

    public override void Start(BrainComponent _owner)
    {
        if (isStarted) return;
        nextTransition = new Transition(new ChaseState(), TranstionCondition);
        isStarted = true;

        detection = _owner.GetComponent<DetectionComponent>();

        Debug.Log("Change State => IdleState");
    }

    public override void Update(BrainComponent _owner)
    {
        // Nothing
    }

    public override void Exit(BrainComponent _owner)
    {
        // Nothing
    }

    public override bool TranstionCondition()
    {
        return detection.IsClose();
    }
}
