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
    [SerializeField] Button CreateCharacterButton;

    [Header("Play Buttons")]
    [SerializeField] Button playButton;

    [Header("Play Options")]
    [SerializeField] GameObject playOptionPanel;
    [SerializeField] Button singleplayerButton;
    [SerializeField] Button createMultiplayerSessionButton;
    [SerializeField] Button joinMultiplayerSessionButton;

    [Header("Quit")]
    [SerializeField] Button quitButton;

    [Header("Settings")]
    [SerializeField] GameObject settingsPanel; // => TODO Behaviour
    [SerializeField] Button settingsButton;

    void Start()
    {
        SetButtons();
    }

    void Update()
    {
        
    }

    void SetButtons()
    {
        //CreateCharacterButton.onClick.AddListener();

        playButton.onClick.AddListener(OpenPlayOptionPanel);

        singleplayerButton.onClick.AddListener(PlaySingleplayer);
        createMultiplayerSessionButton.onClick.AddListener(CreateSession);
        joinMultiplayerSessionButton.onClick.AddListener(JoinSession);

        quitButton.onClick.AddListener(CloseGame);

        //settingsButton.onClick.AddListener();
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
        GameManager.Instance.SetMultiplayer(false);
        GameManager.Instance.CreatePlayer();
    }

    void CreateSession()
    {
        LoadLevel();
        NetworkManager.Singleton.StartHost();
        GameManager.Instance.SetMultiplayer(true);
    }

    void JoinSession()
    {
        LoadLevel();
        NetworkManager.Singleton.StartClient();
        GameManager.Instance.SetMultiplayer(true);
    }
}
