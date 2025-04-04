using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StandingAttackSpell : Spell
{
    public float range = 1.0f;
    public int damage = 0;
    public bool attackOnlyTarget = false;

    public override void Execute(Player _owner, Transform _spellSocket)
    {
        return;
    }
}
