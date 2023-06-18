using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager: MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _confirmationPopUp;

    // Update is called once per frame

    void PauseGame()
    {
        _panel.SetActive(true);
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _panel.SetActive(false);
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Abort()
    {
        _pauseMenu.SetActive(false);
        _confirmationPopUp.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        _pauseMenu.SetActive(false);
        _mainMenuUI.SetActive(true);
    }

    public void ClosePopUp()
    {
        _confirmationPopUp.SetActive(false);
        _pauseMenu.SetActive(true);
    }
}
