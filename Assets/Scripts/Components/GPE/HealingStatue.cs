using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStatue : Interactable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Interaction(Player _player)
    {
        base.Interaction(_player);
        if (isOnCooldown) return;

        StatsComponent _stats = _player.GetComponent<StatsComponent>();
        if (!_stats) return;

        _stats.RestaureHealth(_stats.maxHealth.Value);
        _stats.RestaureMana(_stats.maxMana.Value);

        isOnCooldown = true;
    }
}
