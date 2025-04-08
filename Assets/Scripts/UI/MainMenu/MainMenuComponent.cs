using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuComponent : MonoBehaviour
{
    [SerializeField] MainScreenComponent mainScreen;
    [SerializeField] CharacterSelectionComponent characterSelection;
    [SerializeField] ClassCreationComponent classCreation;

    private void Awake()
    {
        mainScreen = GetComponentInChildren<MainScreenComponent>(true);
        characterSelection = GetComponentInChildren<CharacterSelectionComponent>(true);
        classCreation = GetComponentInChildren<ClassCreationComponent>(true);
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
        });
    }
}
