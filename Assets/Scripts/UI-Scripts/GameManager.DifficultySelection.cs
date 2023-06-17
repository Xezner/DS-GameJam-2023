
using UnityEngine;

public partial class GameManager
{
      public class DifficultySelection
    {
      
    }

    public void BackToMainMenu()
    {
        Difficulty.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
