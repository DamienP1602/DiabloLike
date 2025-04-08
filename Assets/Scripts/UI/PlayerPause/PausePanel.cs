using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] CustomButton resumeButton;
    [SerializeField] CustomButton settingsButton;
    [SerializeField] CustomButton quitGamebutton;

    Player playerRef;

    private void Awake()
    {
        resumeButton.AddLeftClickAction(ResumeGame);
        settingsButton.AddLeftClickAction(OpenSettings);
        quitGamebutton.AddLeftClickAction(QuitGame);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void OpenSettings()
    {
        //TODO open Settigns
    }


    void QuitGame()
    {
        //TODO => save progression
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
