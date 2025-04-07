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

    private void Awake()
    {
        mainScreen = GetComponentInChildren<MainScreenComponent>(true);
        characterSelection = GetComponentInChildren<CharacterSelectionComponent>(true);
    }

    private void Start()
    {
        SetEvent();
    }

    public void SetEvent()
    {
        mainScreen.Button.onClick.AddListener(() =>
        {
            mainScreen.gameObject.SetActive(false);
            characterSelection.gameObject.SetActive(true);
        });
    }
}
