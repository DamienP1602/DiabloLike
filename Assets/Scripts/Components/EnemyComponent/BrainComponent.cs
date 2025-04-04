using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainComponent : MonoBehaviour
{
    FSM fsm = null;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FSM(this, new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        fsm.ComputeFSM();
    }
}
