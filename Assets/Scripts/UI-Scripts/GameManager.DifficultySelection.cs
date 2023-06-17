
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
      public static class DifficultySelection
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
            Instance.Difficulty.SetActive(false);
            Instance.MainMenuUI.SetActive(true);
        }
    }
}
