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
            PauseGame();
        }
    }

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
        SceneManager.LoadScene("UISample");
    }

    public void ClosePopUp()
    {
        _confirmationPopUp.SetActive(false);
        _pauseMenu.SetActive(true);
    }
}
