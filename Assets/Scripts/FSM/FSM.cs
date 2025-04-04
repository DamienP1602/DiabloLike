using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    BrainComponent owner = null;
    State currentState = null;

    public bool Start { get; set; } = false;

    public FSM(BrainComponent _owner, State _startingState)
    {
        owner = _owner;
        currentState = _startingState;
        Start = true;
    }

    public void ComputeFSM()
    {
        if (Start)
        {
            currentState.Start(owner);

            currentState.Update(owner);

            if (currentState.NextTransition.Condition())
            {
                currentState.Exit(owner);
                currentState = currentState.NextTransition.NextState;
            }
        }
    }
}
