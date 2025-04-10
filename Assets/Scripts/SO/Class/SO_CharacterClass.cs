using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
    [Header("Class Data")]
    public int classID;
    public string className;
    public List<string> classInfo;

    public int health;
    public int mana;
    public int strength;
    public int intelligence;
    public int agility;

    public List<SpellCharacterData> allSpells;

    [Header("Graphic Data")]
    public CharacterPrevisualitationComponent characterPrevisualisation;
    public Texture2D classIcon;
    public Color classColor;

    [Header("Animation Data")]
    public RuntimeAnimatorController controller;
}
