using UnityEditor;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] CustomButton resumeButton;
    [SerializeField] CustomButton settingsButton;
    [SerializeField] CustomButton quitGamebutton;

    Player playerRef;

    public void SetPlayerRef(Player _player) => playerRef = _player;

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
        SaveSystem.SaveCharacter(playerRef);

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
