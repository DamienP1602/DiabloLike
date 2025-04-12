using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpellCharacterData<T>
{
    public int minimumLevel;
    public T spell;
}

[CreateAssetMenu(fileName = "Empty Class Data", menuName = "Character Class/Create Class Data")]
public class SO_CharacterClass : ScriptableObject
{
    [Header("Class Data")]
    public int classID;
    public string className;
    public List<string> classInfo;

    public int health;
    public int mana;
    public int strength;
    public int intelligence;
    public int agility;

    public List<SpellCharacterData<Spell>> allSpells;
    public List<SpellCharacterData<Passif>> allPassifs;

    [Header("Graphic Data")]
    public CharacterPrevisualitationComponent characterPrevisualisation;
    public Texture2D classIcon;
    public Color classColor;

    [Header("Animation Data")]
    public RuntimeAnimatorController controller;
}
