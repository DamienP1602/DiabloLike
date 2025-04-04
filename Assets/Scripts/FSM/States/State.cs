using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Transition nextTransition;
    protected bool isStarted = false;

    public Transition NextTransition => nextTransition;

    public abstract void Start(BrainComponent _owner);

    public abstract void Update(BrainComponent _owner);

    public abstract void Exit(BrainComponent _owner);

    public abstract bool TranstionCondition();
}
