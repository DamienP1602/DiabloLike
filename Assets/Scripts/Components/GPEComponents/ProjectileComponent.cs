using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;
    [SerializeField] int damage = 0;

    public void SetOwner(Player _owner) => owner = _owner;

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player)
            return;

        StatsComponent _targetStats = other.GetComponent<StatsComponent>();
        if (!_targetStats) return;

        StatsComponent _stats = owner.GetComponent<StatsComponent>();

        _targetStats.RemoveHealth(damage);

        if (_targetStats.currentHealth.Value <= 0)
        {
            int _experienceGain = _targetStats.RetreiveExperience();
            _stats.GainExperience(_experienceGain);
        }

        Destroy(gameObject);
    }
}
