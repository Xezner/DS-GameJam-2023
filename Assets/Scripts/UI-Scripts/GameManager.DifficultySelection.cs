
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager: MonoBehaviour
{
    [SerializeField] private List<DifficultyDataHolder> _difficultyData;
    public List<DifficultyDataHolder> DifficultyData { get { return _difficultyData; } set { _difficultyData = value; } }
    public DifficultyDataHolder CurrentDifficulty;


    public static void EasyDifficulty()
    {
        SetDifficultyData(Instance.DifficultyData[0]);
    }

    public static void NormalDifficulty()
    {
        SetDifficultyData(Instance.DifficultyData[1]);
    }

    public static void HardDifficulty()
    {
        SetDifficultyData(Instance.DifficultyData[2]);
    }

    public static void SetDifficultyData(DifficultyDataHolder difficultyData)
    {
        Instance.CurrentDifficulty = difficultyData;
        Instance.CurrentDifficulty.EnemyData.EnemyHealth = difficultyData.EnemyHealth;
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
