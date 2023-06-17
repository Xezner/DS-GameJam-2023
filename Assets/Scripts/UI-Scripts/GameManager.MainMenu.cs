using UnityEngine;

public partial class GameManager
{
    public GameObject Difficulty;
    public GameObject MainMenuUI;

    public void StartGame()
    {
        //Load difficulty setting
        MainMenuUI.SetActive(false);
        Difficulty.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
