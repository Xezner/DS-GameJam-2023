using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager: MonoBehaviour
{
    void GameOver()
    {
        Instance.GameOverUI.SetActive(true);
    }

    public void Retry()
    {
        _gameOver = false;
        SceneManager.UnloadSceneAsync(Scenes.GameSceneMain.ToString());
        Instance.GameOverUI.SetActive(false);
        Instance._difficulty.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        _gameOver = false;
        SceneManager.UnloadSceneAsync(Scenes.GameSceneMain.ToString());
        Instance.GameOverUI.SetActive(false);
        Instance._mainMenuUI.SetActive(true);

    }
}
