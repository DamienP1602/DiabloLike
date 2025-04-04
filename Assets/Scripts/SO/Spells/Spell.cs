using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public Texture2D icon;
    public int manaCost = 0;
    public float cooldown = 0;
    public float currentCooldown = 0;
    public float castTime = 0;
    public bool isOnCooldown = false;
    public string animationSpellName = "";

    public abstract void Execute(Player _owner, Transform _spellSocket);

    public void ResetSpell()
    {
        currentCooldown = 0.0f;
        isOnCooldown = false;
    }
}
