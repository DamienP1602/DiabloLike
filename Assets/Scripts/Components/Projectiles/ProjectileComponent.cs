using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField] Player owner;
    [SerializeField] SpellDamageData damage;

    public void SetOwner(Player _owner) => owner = _owner;

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player)
            return;

        StatsComponent _targetStats = other.GetComponent<StatsComponent>();
        if (!_targetStats) return;

        StatsComponent _statsOwner = owner.GetComponent<StatsComponent>();
        if (!_statsOwner) return;

        int _spellDamage = damage.GetDamage(_statsOwner);
        int _damageDeals = _targetStats.RetreiveResistanceReduction(_spellDamage);

        _targetStats.RemoveHealth(_damageDeals);

        if (_targetStats.currentHealth.Value <= 0)
        {
            int _experienceGain = _targetStats.RetreiveExperience();
            _statsOwner.GainExperience(_experienceGain);
        }

        Destroy(gameObject);
    }
}
