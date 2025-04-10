using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelComponent : MonoBehaviour
{
    [SerializeField] CharacterInfoComponent prefab;
    [SerializeField] List<CharacterInfoComponent> allCharacter = new List<CharacterInfoComponent>();

    public int CharacterAmount => allCharacter.Count;

    public void UpdateCharacterOnSaveData(Action<CharacterSaveData> _event, List<SO_CharacterClass> _classesData)
    {
        SaveData _data = SaveSystem.Data;

        foreach (CharacterInfoComponent _character in allCharacter)
        {
            Destroy(_character.gameObject);
        }
        allCharacter.Clear();

        foreach (CharacterSaveData _characterData in _data.allCharacters)
        {
            CharacterInfoComponent _newCharacter = Instantiate(prefab,transform);
            _newCharacter.SetClassText(_characterData.className);
            _newCharacter.SetLevelText(_characterData.level.ToString());
            _newCharacter.SetNameText(_characterData.name);
            _newCharacter.Button.AddLeftClickAction(() => _event?.Invoke(_characterData));

            foreach (SO_CharacterClass _classData in _classesData)
            {
                if (_classData.classID == _characterData.classID)
                {
                    _newCharacter.SetIcon(_classData.classIcon);
                    _newCharacter.SetClassTextColor(_classData.classColor);
                    break;
                }
            }

            allCharacter.Add(_newCharacter);
        }
    }

    public void RemoveCharacter(string _name)
    {
        foreach (CharacterInfoComponent _character in allCharacter)
        {
            if (_character.CharacterName == _name)
            {
                Destroy(_character.gameObject);
                allCharacter.Remove(_character);
                return;
            }
        }
    }
}
