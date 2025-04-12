using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveItemData
{
    public int ID;
    public string name;
    public int amount;

    public SaveItemData(int _id, string _name, int _amount)
    {
        ID = _id;
        name = _name;
        amount = _amount;
    }
}

public struct SaveSpellData
{
    public int ID;
    public string name;

    public SaveSpellData(int _id, string _name)
    {
        ID = _id;
        name = _name;
    }
}

public class SaveData
{
    public List<CharacterSaveData> allCharacters = new List<CharacterSaveData>();
}

public struct CharacterSaveData
{
    public string name;
    public int classID;
    public string className;

    public int gold;
    public List<SaveItemData> itemInInventory;
    public List<SaveItemData> itemEquiped;

    public List<SaveSpellData> spellsEquiped;
    public List<SaveSpellData> passifEquiped;
    
    public int level;
    public int experience;
    public int statPoints;
    
    public int health;
    public int mana;

    public int armor;
    public int resistance;

    public int damage;
    public int strength;
    public int intelligence;
    public int agility;
}
