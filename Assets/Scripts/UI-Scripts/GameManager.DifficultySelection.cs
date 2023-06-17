
using UnityEngine;

public partial class GameManager
{
      public static class DifficultySelection
    {
        public static void BackToMainMenu()
        {
            Instance.Difficulty.SetActive(false);
            Instance.MainMenuUI.SetActive(true);
        }
    }
}
