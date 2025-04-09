using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelComponent : MonoBehaviour
{
    [SerializeField] CharacterInfoComponent prefab;
    [SerializeField] List<CharacterInfoComponent> allCharacter = new List<CharacterInfoComponent>();

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
                    _newCharacter.SetBackgroundColor(_classData.classColor);
                    _newCharacter.SetIcon(_classData.classIcon);
                    break;
                }
            }

            allCharacter.Add(_newCharacter);
        }
    }
}
