using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionComponent : MonoBehaviour
{
    [Header("All Classes To Draw")]
    public Action OnCharacterSelection;
    CharacterPrevisualitationComponent characterMesh;
    CharacterSaveData? characterSelected;
    SO_CharacterClass classSelected;
    public List<SO_CharacterClass> allClasses;

    [Header("Character Creation Side")]
    [SerializeField] CharacterPanelComponent charactersPanel;
    [SerializeField] CustomButton createCharacterButton;
    [SerializeField] CustomButton deleteCharacterButton;

    [Header("Delete Options")]
    [SerializeField] GameObject deleteVerificationPanel;
    [SerializeField] TMP_Text deleteText;
    [SerializeField] CustomButton yesButton;
    [SerializeField] CustomButton noButton;

    [Header("Play Buttons")]
    [SerializeField] CustomButton playButton;

    [Header("Quit")]
    [SerializeField] CustomButton quitButton;

    [Header("Settings")]
    [SerializeField] GameObject settingsPanel; // => TODO Behaviour
    [SerializeField] CustomButton settingsButton;

    public CustomButton CreateCharacterButton => createCharacterButton;
    public CharacterPanelComponent CharacterPanel => charactersPanel;

    void Start()
    {
        SetButtons();
    }

    void Update()
    {
        
    }

    void SetButtons()
    {
        playButton.AddLeftClickAction(OpenPlayOptionPanel);

        deleteCharacterButton.AddLeftClickAction(OpenDeletePanel);
        yesButton.AddLeftClickAction(DeleteCharacter);
        noButton.AddLeftClickAction(CloseDeletePanel);

        quitButton.AddLeftClickAction(CloseGame);

        settingsButton.AddLeftClickAction(() => print("Settings"));
    }

    public void DrawCreationButton()
    {
        if (charactersPanel.CharacterAmount == 4)
        {
            createCharacterButton.SetInteractable(false);
        }
    }

    void CloseGame()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    void OpenPlayOptionPanel()
    {
        PlaySingleplayer();
        //playOptionPanel.SetActive(true);
    }

    void PlaySingleplayer()
    {
        if (characterSelected == null) return;

        SceneManager.LoadScene("SampleScene");

        GameManager.Instance.CreatePlayer(characterSelected.Value, classSelected);
    }

    //void CreateSession()
    //{
    //    LoadLevel();
    //    // Load Selected Character
    //    NetworkManager.Singleton.StartHost();
    //    GameManager.Instance.SetMultiplayer(true);
    //}

    //void JoinSession()
    //{
    //    LoadLevel();
    //    // Load Selected Character
    //    NetworkManager.Singleton.StartClient();
    //    GameManager.Instance.SetMultiplayer(true);
    //}

    public void SelectCharacter(CharacterSaveData _character)
    {
        if (characterMesh)
            Destroy(characterMesh.gameObject);

        foreach (SO_CharacterClass _data in allClasses)
        {
            if (_data.className == _character.className)
            {
                characterMesh = Instantiate(_data.characterPrevisualisation, transform);
                characterSelected = _character;
                classSelected = _data;
            }
        }
        deleteCharacterButton.SetInteractable(true);
    }

    public void OpenDeletePanel()
    {
        deleteVerificationPanel.SetActive(true);
        deleteText.text = $"Delete {characterSelected?.name} ?";
    }

    public void CloseDeletePanel()
    {
        deleteVerificationPanel.SetActive(false);
    }

    public void DeleteCharacter()
    {
        Destroy(characterMesh.gameObject);
        charactersPanel.RemoveCharacter(characterSelected?.name);
        SaveSystem.DeleteCharacter(characterSelected.Value);
        characterSelected = null;
        CloseDeletePanel();
        deleteCharacterButton.SetInteractable(false);
    }
}
