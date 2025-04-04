using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectionComponent), typeof(BrainComponent), typeof(DropComponent))]
public class BaseEnemy : BaseCharacter
{
    DetectionComponent detection = null;
    BrainComponent brain = null;
    DropComponent lootTable = null;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void Init()
    {
        base.Init();
        detection = GetComponent<DetectionComponent>();
        brain = GetComponent<BrainComponent>();
        lootTable = GetComponent<DropComponent>();
    }
    
    override protected void EventAssignation()
    {
        base.EventAssignation();
        statsComponent.OnDeath += lootTable.ComputeLootTable;
    }
}
