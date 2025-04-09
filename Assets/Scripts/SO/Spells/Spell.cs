using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public int ID;
    public Texture2D icon;
    public int manaCost;
    public float cooldown;
    public float currentCooldown;
    public float castTime;
    public bool isOnCooldown;
    public string animationSpellName;

    public abstract void Execute(Player _owner, Transform _spellSocket);

    public void ResetSpell()
    {
        currentCooldown = 0.0f;
        isOnCooldown = false;
    }
}
