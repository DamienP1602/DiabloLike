using JetBrains.Annotations;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassCreationComponent : MonoBehaviour
{
    [Header("Base Informations")]
    [SerializeField] TMP_Text className;
    [SerializeField] TMP_InputField characterNameInputField;
    [SerializeField] CustomButton createCharacterButton;
    [SerializeField] TMP_Text classInfoText;
    [SerializeField] GameObject classButtonPanel;
    [SerializeField] CustomButton returnButton;

    [Header("Error Panel")]
    [SerializeField] GameObject ErrorCreationPanel;
    [SerializeField] TMP_Text ErrorText;
    [SerializeField] CustomButton continueButton;

    [Header("Prefabs")]
    [SerializeField] CustomButton ButtonClassRef;
    public List<SO_CharacterClass> allClasses;

    CharacterPrevisualitationComponent characterMesh;
    SO_CharacterClass selectedClass;
    public bool isCharacterCreated;

    public CustomButton ReturnButton => returnButton;
    public CustomButton CreateCharacterButton => createCharacterButton;

    private void Awake()
    {
        foreach (SO_CharacterClass _data in allClasses)
        {
            CustomButton _newButton = Instantiate(ButtonClassRef, classButtonPanel.transform);
            TMP_Text _buttonText = _newButton.GetComponentInChildren<TMP_Text>();
            _buttonText.text = _data.className;

            _newButton.AddLeftClickAction(() =>
            {
                if (characterMesh)
                    Destroy(characterMesh.gameObject);

                className.text = _data.className;
                characterMesh = Instantiate(_data.characterPrevisualisation, transform);
                selectedClass = _data;

                classInfoText.text = "";
                foreach(string _str in _data.classInfo)
                {
                    classInfoText.text += _str + "\n";
                }
            });
        }
        continueButton.AddLeftClickAction(() => ErrorCreationPanel.gameObject.SetActive(false));
    }

    public void ResetValues()
    {
        className.text = "";
        if (characterMesh)
            Destroy(characterMesh.gameObject);
        classInfoText.text = "";
        characterNameInputField.text = "";

    }

    public void CreateCharacter()
    {
        string _characterName = characterNameInputField.text;
        if (selectedClass == null)
        {
            ErrorCreationPanel.gameObject.SetActive(true);
            ErrorText.text = "You need to select a Class";
            return;
        }
        else if (string.IsNullOrEmpty(_characterName))
        {
            ErrorCreationPanel.gameObject.SetActive(true);
            ErrorText.text = "You need to enter a Name";
            return;
        }
        else if (SaveSystem.HasCharacterAlreadyThisName(_characterName))
        {
            ErrorCreationPanel.gameObject.SetActive(true);
            ErrorText.text = "This Name is already selected";
            return;
        }

        SaveSystem.SaveCharacter(selectedClass, _characterName);
        isCharacterCreated = true;
    }
}
