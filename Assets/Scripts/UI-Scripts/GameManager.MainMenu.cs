using UnityEngine;

public partial class GameManager
{
    public static void StartGame()
    {
        //Load difficulty setting
        Instance._mainMenuUI.SetActive(false);
        Instance._difficulty.SetActive(true);
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}
