using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passif : ScriptableObject
{
    [Header("Base Passif Informations")]
    public int ID;
    public Texture2D icon;
    public bool IsAlwaysActive;
    public float cooldown;
    public float currentCooldown;

    public abstract void Activate(Player _owner);

    public abstract void Desactivate(Player _owner);
}
