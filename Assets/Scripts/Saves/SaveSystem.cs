using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem
{
    static SaveData data;

    public static SaveData Data => data;

    public static void InitSaveSystem()
    {
        data = new SaveData();
        string _path = GetPath();
        string _jsonData = GetJsonData(_path);
        if (string.IsNullOrEmpty(_jsonData)) return;

        data = JsonConvert.DeserializeObject<SaveData>(_jsonData);
    }

    public static void SaveGame()
    {
        string _path = GetPath();


    }

    public static void SaveCharacter(Player _character)
    {
        string _path = GetPath();
        //string _data = GetJsonData(_path);
        string _characterData = CreateJsonData(_character);
    }

    public static void SaveCharacter(SO_CharacterClass _character, string _characterName)
    {
        string _path = GetPath();
        CharacterSaveData _characterData = CreateData(_character, _characterName);
        data.allCharacters.Add(_characterData);

        string _jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

        WriteInFile(_path, _jsonData);
    }

    static string GetPath()
    {
        string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _path += "\\GhostsCallSaves\\Saves.json";

        return _path;
    }

    static string GetJsonData(string _path)
    {
        return File.ReadAllText(_path);
    }

    static void WriteInFile(string _path, string _data)
    {
        File.WriteAllText(_path, _data);
    }

    static string CreateJsonData(Player _character)
    {
        CharacterSaveData _data = default;  

        _data.name = _character.CharacterName;
        _data.classID = _character.ClassComponent.ClassData.classID;
        _data.className = _character.ClassComponent.ClassData.className;


        List<ItemStored> _allItems = _character.Inventory.AllItems;
        foreach (ItemStored _itemStored in _allItems)
        {
            BaseItem _item = _itemStored.item;
            if (!_item) continue;

            _data.itemIDInventory.Add(new SaveItemData(_item.ID, _item.itemName, _itemStored.amount));
        }

        List<ItemStored> _allItemsEquiped = _character.Inventory.AllItemsEquiped;
        foreach (ItemStored _itemEquiped in _allItemsEquiped)
        {
            BaseItem _item = _itemEquiped.item;
            if (!_item) continue;

            _data.itemIDEquiped.Add(new SaveItemData(_item.ID, _item.itemName, _itemEquiped.amount));
        }

        List<Spell> _characterSpells = _character.SpellComponent.Spells;
        foreach (Spell _spell in _characterSpells)
        {
            _data.spellsIDEquiped.Add(_spell.ID);
        }

        List<Spell> _characterPassifs = _character.SpellComponent.PassifsSpells ;
        foreach (Spell _passif in _characterPassifs)
        {
            _data.spellsIDEquiped.Add(_passif.ID);
        }

        _data.level = _character.StatsComponent.level.Value;
        _data.experience = _character.StatsComponent.experience.Value;
        _data.statPoints = _character.StatsComponent.statPoints.Value;

        _data.health = _character.StatsComponent.maxHealth.Value;
        _data.mana = _character.StatsComponent.maxMana.Value;

        _data.armor = _character.StatsComponent.armor.Value;
        _data.resistance = _character.StatsComponent.resistance.Value;

        _data.strength = _character.StatsComponent.strength.Value;
        _data.intelligence = _character.StatsComponent.intelligence.Value;
        _data.agility = _character.StatsComponent.agility.Value;

        return JsonConvert.SerializeObject(_data, Formatting.Indented);
    }

    static CharacterSaveData CreateData(SO_CharacterClass _character, string _characterName)
    {
        CharacterSaveData _data = default;

        _data.name = _characterName;
        _data.classID = _character.classID;
        _data.className = _character.className;

        _data.itemIDInventory = new List<SaveItemData>();
        _data.itemIDEquiped = new List<SaveItemData>();

        _data.spellsIDEquiped = new List<int>();
        _data.passifIDEquiped = new List<int>();

        _data.level = 1;
        _data.experience = 0;
        _data.statPoints = 0;

        _data.health = _character.health;
        _data.mana = _character.mana;

        _data.armor = 0;
        _data.resistance = 0;

        _data.strength = _character.strength;
        _data.intelligence = _character.intelligence;
        _data.agility = _character.agility;

        return _data;
        //return JsonConvert.SerializeObject(_data,Formatting.Indented);
    }
}
