using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuComponent : MonoBehaviour
{
    [SerializeField] MainScreenComponent mainScreen;
    [SerializeField] CharacterSelectionComponent characterSelection;
    [SerializeField] ClassCreationComponent classCreation;
    [SerializeField] List<SO_CharacterClass> allClasses;

    private void Awake()
    {
        SaveSystem.InitSaveSystem();

        mainScreen = GetComponentInChildren<MainScreenComponent>(true);
        characterSelection = GetComponentInChildren<CharacterSelectionComponent>(true);
        classCreation = GetComponentInChildren<ClassCreationComponent>(true);

        characterSelection.allClasses = allClasses;
        classCreation.allClasses = allClasses;
    }

    private void Start()
    {
        SetEvent();
    }

    public void SetEvent()
    {
        mainScreen.Button.AddLeftClickAction(() =>
        {
            mainScreen.gameObject.SetActive(false);
            characterSelection.gameObject.SetActive(true);
            characterSelection.CharacterPanel.UpdateCharacterOnSaveData(characterSelection.SelectCharacter, allClasses);
            characterSelection.DrawCreationButton();
        });
        characterSelection.CreateCharacterButton.AddLeftClickAction(() =>
        {
            characterSelection.gameObject.SetActive(false);
            classCreation.gameObject.SetActive(true);
        });
        classCreation.ReturnButton.AddLeftClickAction(() =>
        {
            classCreation.gameObject.SetActive(false);
            classCreation.ResetValues();
            characterSelection.gameObject.SetActive(true);
            characterSelection.DrawCreationButton();
        });
        classCreation.CreateCharacterButton.AddLeftClickAction(classCreation.CreateCharacter);
        classCreation.CreateCharacterButton.AddLeftClickAction(() =>
        {
            if (classCreation.isCharacterCreated)
            {
                classCreation.gameObject.SetActive(false);
                classCreation.ResetValues();
                characterSelection.gameObject.SetActive(true);
                characterSelection.CharacterPanel.UpdateCharacterOnSaveData(characterSelection.SelectCharacter, allClasses);
                classCreation.isCharacterCreated = false;
                characterSelection.DrawCreationButton();
            }

        });
    }
}
