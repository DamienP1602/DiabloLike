using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpellCharacterData
{
    public int minimumLevel;
    public Spell spell;
}

[CreateAssetMenu(fileName = "Empty Class Data", menuName = "Character Class/Create Class Data")]
public class SO_CharacterClass : ScriptableObject
{
    public int classID;
    public string className;
    public CharacterPrevisualitationComponent characterModel;
    public List<string> classInfo;

    public int health;
    public int mana;
    public int strength;
    public int intelligence;
    public int agility;

    public List<SpellCharacterData> allSpells;

    public Texture2D classIcon;
    public Color classColor;
}
