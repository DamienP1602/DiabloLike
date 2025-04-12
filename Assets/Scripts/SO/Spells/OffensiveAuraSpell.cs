using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Offensive Aura Spell", menuName = "Spell/Aura/Create new Offensive Aura Spell")]
public class OffensiveAuraSpell : Passif
{
    [Header("Specific Spell Informations")]
    [SerializeField] SpellDamageData damage;
    [SerializeField] float range;

    public override void Activate(Player _owner)
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

    public override void Desactivate(Player _owner)
    {
        return;
    }
}
