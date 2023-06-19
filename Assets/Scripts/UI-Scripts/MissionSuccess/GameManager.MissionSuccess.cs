using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager: MonoBehaviour
{
    void MissionComplete()
    {
        Instance._missionSuccess.SetActive(true);
    }

    public void Continue()
    {
        _gameOver = false;
        SceneManager.UnloadSceneAsync(Scenes.GameSceneMain.ToString());
        Instance._missionSuccess.SetActive(false);
        Instance._difficulty.SetActive(true);
        
    }

    public void SuccessToMainMenu()
    {
        SceneManager.UnloadSceneAsync(Scenes.GameSceneMain.ToString());
        Instance._missionSuccess.SetActive(false);
        Instance._mainMenuUI.SetActive(true);
    }
}
