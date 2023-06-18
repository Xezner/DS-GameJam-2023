
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    public static void EasyDifficulty()
    {
        Instance._mainMenuUI.SetActive(false);
        Instance._difficulty.SetActive(false);
        SceneManager.LoadScene(Scenes.LoreScreen.ToString(), LoadSceneMode.Additive);
    }

    public static void NormalDifficulty()
    {
        Instance._mainMenuUI.SetActive(false);
        Instance._difficulty.SetActive(false);
        SceneManager.LoadScene(Scenes.LoreScreen.ToString(), LoadSceneMode.Additive);
    }

    public static void HardDifficulty()
    {
        Instance._mainMenuUI.SetActive(false);
        Instance._difficulty.SetActive(false);
        SceneManager.LoadScene(Scenes.LoreScreen.ToString(), LoadSceneMode.Additive);
    }

    public static void BackToMainMenu()
    {
        Instance._difficulty.SetActive(false);
        Instance._mainMenuUI.SetActive(true);
    }
}
