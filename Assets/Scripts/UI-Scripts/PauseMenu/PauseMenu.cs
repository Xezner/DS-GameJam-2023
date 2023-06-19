using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _confirmationPopUp;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.LogError($"Current Scene{currentScene.name}");
            PauseGame();
        }
    }

    void PauseGame()
    {
        _panel.SetActive(true);
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _panel.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    public void Abort()
    {
        _pauseMenu.SetActive(false);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ClosePopUp()
    {
        _confirmationPopUp.SetActive(false);
        _pauseMenu.SetActive(true);
    }
}
