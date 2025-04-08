using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionComponent : MonoBehaviour
{
    [Header("Character Creation Side")]
    [SerializeField] GameObject charactersPanel; // => TODO Behaviour
    [SerializeField] CustomButton createCharacterButton;

    [Header("Play Buttons")]
    [SerializeField] CustomButton playButton;

    [Header("Play Options")]
    [SerializeField] GameObject playOptionPanel;
    [SerializeField] CustomButton singleplayerButton;
    [SerializeField] CustomButton createMultiplayerSessionButton;
    [SerializeField] CustomButton joinMultiplayerSessionButton;

    [Header("Quit")]
    [SerializeField] CustomButton quitButton;

    [Header("Settings")]
    [SerializeField] GameObject settingsPanel; // => TODO Behaviour
    [SerializeField] CustomButton settingsButton;

    public CustomButton CreateCharacterButton => createCharacterButton;

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

        singleplayerButton.AddLeftClickAction(PlaySingleplayer);
        createMultiplayerSessionButton.AddLeftClickAction(CreateSession);
        joinMultiplayerSessionButton.AddLeftClickAction(JoinSession);

        quitButton.AddLeftClickAction(CloseGame);

        settingsButton.AddLeftClickAction(() => print("Settings"));
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
        playOptionPanel.SetActive(true);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("SampleScene");

    }

    void PlaySingleplayer()
    {
        LoadLevel();
        // Load Selected Character
        GameManager.Instance.SetMultiplayer(false);
        GameManager.Instance.CreatePlayer();
    }

    void CreateSession()
    {
        LoadLevel();
        // Load Selected Character
        NetworkManager.Singleton.StartHost();
        GameManager.Instance.SetMultiplayer(true);
    }

    void JoinSession()
    {
        LoadLevel();
        // Load Selected Character
        NetworkManager.Singleton.StartClient();
        GameManager.Instance.SetMultiplayer(true);
    }
}
