using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _difficulty;
    [SerializeField] private GameObject _mainMenuUI;
    public GameObject Difficulty { get { return _difficulty; } set { value = _difficulty; } }
    public GameObject MainMenuUI { get { return _mainMenuUI; } set { value = _mainMenuUI; } }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        MainMenu.StartGame();
    }

    public void QuitGame()
    {
        MainMenu.QuitGame();
    }

    public void BackToMainMenu()
    {
        DifficultySelection.BackToMainMenu();
    }

    public void EasyDifficulty()
    {
        DifficultySelection.EasyDifficulty();
    }

    public void NormalDifficulty()
    {
        DifficultySelection.NormalDifficulty();
    }

    public void HardDifficulty()
    {
        DifficultySelection.HardDifficulty();
    }
}
