using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(fileName = "Slash Spell", menuName = "Spell/Create new Slash Spell")]
public class SlashSpell : StandingAttackSpell
{
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


                int _damage = damage + _statsOwner.damage.Value + (_statsOwner.strength.Value / 2);
                _targetStats.RemoveHealth(_damage);

                if (_targetStats.currentHealth.Value <= 0)
                {
                    int _experienceGain = _targetStats.RetreiveExperience();

                    _statsOwner.GainExperience(_experienceGain);
                }
            }
        }
    }
}
