
using UnityEngine;

public partial class GameManager
{
    public void BackToMainMenu()
    {
        Difficulty.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
