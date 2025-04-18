using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class SaveSystem
{
    static SaveData data;

    public static SaveData Data => data;

    public static void InitSaveSystem()
    {
        data = new SaveData();
        string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GhostsCallSaves\\";
        if (!IsFileExisting(_path))
            Directory.CreateDirectory(_path);

        _path += "\\Saves.json";
        if (!IsFileExisting(_path))
            File.WriteAllText(_path, "");

        string _jsonData = GetJsonData(_path);
        if (string.IsNullOrEmpty(_jsonData)) return;

        data = JsonConvert.DeserializeObject<SaveData>(_jsonData);
    }

    public static bool HasCharacterAlreadyThisName(string _name)
    {
        foreach (CharacterSaveData _characterData in data.allCharacters)
        {
            if (_characterData.name == _name)
                return true;
        }
        return false;
    }

    public static void SaveCharacter(Player _character)
    {
        string _path = GetPath();
        CharacterSaveData _characterData = CreateData(_character);

        int _size = data.allCharacters.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            CharacterSaveData _currentSavedCharacter = data.allCharacters[_i];
            if (_currentSavedCharacter.name == _character.CharacterName)
            {
                data.allCharacters.Remove(_currentSavedCharacter);
                data.allCharacters.Add(_characterData);
                break;
            }
        }

        string _jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        WriteInFile(_path, _jsonData);
    }

    public static void SaveCharacter(SO_CharacterClass _character, string _characterName)
    {
        string _path = GetPath();
        CharacterSaveData _characterData = CreateData(_character, _characterName);
        data.allCharacters.Add(_characterData);

        string _jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

        WriteInFile(_path, _jsonData);
    }

    static bool IsFileExisting(string _path)
    {
        bool _exist = File.Exists(_path);
        return _exist;
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

    static CharacterSaveData CreateData(Player _character)
    {
        CharacterSaveData _data = default;  

        _data.name = _character.CharacterName;
        _data.classID = _character.ClassComponent.ClassData.classID;
        _data.className = _character.ClassComponent.ClassData.className;

        _data.gold = _character.Inventory.Gold;
        List<ItemStored> _allItems = _character.Inventory.AllItems;
        _data.itemInInventory = new List<SaveItemData>();
        foreach (ItemStored _itemStored in _allItems)
        {
            BaseItem _item = _itemStored.item;
            if (!_item) continue;

            _data.itemInInventory.Add(new SaveItemData(_item.ID, _item.itemName, _itemStored.amount));
        }

        List<ItemStored> _allItemsEquiped = _character.Inventory.AllItemsEquiped;
        _data.itemEquiped = new List<SaveItemData>();
        foreach (ItemStored _itemEquiped in _allItemsEquiped)
        {
            BaseItem _item = _itemEquiped.item;
            if (!_item) continue;

            _data.itemEquiped.Add(new SaveItemData(_item.ID, _item.itemName, _itemEquiped.amount));
        }

        List<Spell> _characterSpells = _character.SpellComponent.Spells;
        _data.spellsEquiped = new List<SaveSpellData>();
        foreach (Spell _spell in _characterSpells)
        {
            _data.spellsEquiped.Add(new SaveSpellData(_spell.ID, _spell.name));
        }

        List<Passif> _characterPassifs = _character.SpellComponent.Passifs;
        _data.passifEquiped = new List<SaveSpellData>();
        foreach (Passif _passif in _characterPassifs)
        {
            _data.passifEquiped.Add(new SaveSpellData(_passif.ID, _passif.name));
        }

        _data.level = _character.StatsComponent.level.Value;
        _data.experience = _character.StatsComponent.experience.Value;
        _data.statPoints = _character.StatsComponent.statPoints.Value;

        _data.health = _character.StatsComponent.maxHealth.Value;
        _data.mana = _character.StatsComponent.maxMana.Value;

        _data.armor = _character.StatsComponent.armor.Value;
        _data.resistance = _character.StatsComponent.resistance.Value;

        _data.damage = _character.StatsComponent.damage.Value;
        _data.strength = _character.StatsComponent.strength.Value;
        _data.intelligence = _character.StatsComponent.intelligence.Value;
        _data.agility = _character.StatsComponent.agility.Value;

        return _data;
    }

    static CharacterSaveData CreateData(SO_CharacterClass _character, string _characterName)
    {
        CharacterSaveData _data = default;

        _data.name = _characterName;
        _data.classID = _character.classID;
        _data.className = _character.className;

        _data.gold = 0;
        _data.itemInInventory = new List<SaveItemData>();
        _data.itemEquiped = new List<SaveItemData>();

        _data.spellsEquiped = new List<SaveSpellData>();
        _data.passifEquiped = new List<SaveSpellData>();

        _data.level = 1;
        _data.experience = 0;
        _data.statPoints = 0;

        _data.health = _character.health;
        _data.mana = _character.mana;

        _data.armor = 0;
        _data.resistance = 0;

        _data.damage = 1;
        _data.strength = _character.strength;
        _data.intelligence = _character.intelligence;
        _data.agility = _character.agility;

        return _data;
    }

    public static void DeleteCharacter(CharacterSaveData _characterSelected)
    {
        int _size = data.allCharacters.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            CharacterSaveData _character = data.allCharacters[_i];

            if (_character.name == _characterSelected.name)
            {
                data.allCharacters.Remove(_character);
                break;
            }
        }

        string _path = GetPath();
        string _jsonData = JsonConvert.SerializeObject(data,Formatting.Indented);
        File.WriteAllText(_path, _jsonData);
    }
}
