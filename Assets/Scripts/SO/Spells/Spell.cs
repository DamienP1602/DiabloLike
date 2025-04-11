using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum StatsUsed
{
    STRENGTH,
    INTELLIGENCE,
    AGILITY
}

[Serializable]
public struct SpellDamageData
{
    public StatsUsed statUsed;
    public int statDivisionValue;    
    public int damage;

    public int GetDamage(StatsComponent _stats)
    {
        int[] _statsUsed = { _stats.strength.Value, _stats.intelligence.Value, _stats.agility.Value };
        int _value = _statsUsed[(int)statUsed];

        _value = _value / statDivisionValue;

        return damage + _value;
    }
}

public abstract class Spell : ScriptableObject
{
    [Header("Base Spell Informations")]
    public int ID;
    public Texture2D icon;
    public int manaCost;
    public float cooldown;
    public float currentCooldown;
    public float castTime;
    public bool isOnCooldown;
    public string animationSpellName;
    public bool needToRotatePlayer;

    public abstract void Execute(Player _owner, Transform _spellSocket);

    public void ResetSpell()
    {
        currentCooldown = 0.0f;
        isOnCooldown = false;
    }
}
