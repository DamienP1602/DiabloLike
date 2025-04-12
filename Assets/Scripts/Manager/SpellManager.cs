using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    [SerializeField] List<Spell> allSpells;
    [SerializeField] List<Passif> allPassifs;

    public List<Spell> AllSpells => allSpells;
    public List<Passif> AllPassifs => allPassifs;

    protected override void Awake()
    {
        base.Awake();

        foreach (Spell _spell in allSpells)
        {
            _spell.ResetSpell();
        }
        foreach (Passif _passif in allPassifs)
        {
            _passif.currentCooldown = 0.0f;
        }
    }
}
