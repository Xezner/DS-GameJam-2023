
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    public static void EasyDifficulty()
    {
        SceneManager.LoadScene(1);
    }

    public static void NormalDifficulty()
    {
        SceneManager.LoadScene(1);
    }

    public static void HardDifficulty()
    {
        SceneManager.LoadScene(1);
    }

    public static void BackToMainMenu()
    {
        Instance._difficulty.SetActive(false);
        Instance._mainMenuUI.SetActive(true);
    }
}
