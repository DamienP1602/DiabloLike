using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Throw Spell", menuName = "Spell/Create new Throwing Spell")]
public class ThrowSpell : Spell
{
    public ProjectileComponent projectile;
    public float lifespan = 3.0f;

    public override void Execute(Player _owner, Transform _spellSocket)
    {
        ProjectileComponent _projectile = Instantiate(projectile, _spellSocket.transform.position,_owner.transform.rotation);
        _projectile.SetOwner(_owner);
        Destroy(_projectile, lifespan);
    }
}
