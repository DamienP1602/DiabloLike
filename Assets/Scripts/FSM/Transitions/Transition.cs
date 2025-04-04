using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    State nextState;
    Func<bool> condition;

    public State NextState => nextState;
    public Func<bool> Condition => condition;

    public Transition(State _nextState, Func<bool> _condition)
    {
        nextState = _nextState;
        condition = _condition;
    }
}
