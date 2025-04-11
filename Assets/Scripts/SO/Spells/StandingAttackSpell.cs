using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Standing Spell", menuName = "Spell/Create new Standing Spell")]
public class StandingAttackSpell : Spell
{
    [Header("Specific Spell Informations")]
    public float range;
    public SpellDamageData damage;
    public bool attackOnlyTarget;

    public override void Execute(Player _owner, Transform _spellSocket)
    {
        RaycastHit[] _allTargetFound = Physics.SphereCastAll(_owner.transform.position, range, Vector3.forward);

        if (_allTargetFound.Length <= 0) return;

        foreach (RaycastHit _hit in _allTargetFound)
        {
            if (_hit.collider.GetComponent<BaseEnemy>() is BaseEnemy _enemy)
            {
                StatsComponent _targetStats = _enemy.GetComponent<StatsComponent>();
                StatsComponent _statsOwner = _owner.GetComponent<StatsComponent>();
                if (!_targetStats || !_statsOwner) return;

                int _spellDamage = damage.GetDamage(_statsOwner);
                int _damageDeals = _targetStats.RetreiveResistanceReduction(_spellDamage);

                _targetStats.RemoveHealth(_damageDeals);

                if (_targetStats.currentHealth.Value <= 0)
                {
                    int _experienceGain = _targetStats.RetreiveExperience();

                    _statsOwner.GainExperience(_experienceGain);
                }
            }
        }
    }
}
