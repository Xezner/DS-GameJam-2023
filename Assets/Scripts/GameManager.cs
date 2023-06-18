using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public partial class GameManager : MonoBehaviour
{
    public enum Scenes
    {
        StartScreen,
        LoreScreen,
        GameScene,
    }
    public static GameManager Instance;

    private bool _gameOver = false;

    [SerializeField] private GameObject _difficulty;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _missionSuccess;
    public GameObject Difficulty { get { return _difficulty; } set { value = _difficulty; } }
    public GameObject MainMenuUI { get { return _mainMenuUI; } set { value = _mainMenuUI; } }
    public GameObject GameOverUI { get { return _gameOverUI; } set { value = _gameOverUI; } }
    public GameObject MissionSuccess { get { return _missionSuccess; } set { value = _gameOverUI; } }

    public bool PlayMode;

    private void Awake()
    {
        /*DontDestroyOnLoad(this.gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }*/
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            SceneManager.UnloadSceneAsync(Scenes.LoreScreen.ToString());
            _gameOver = true;
        }
        if (_gameOver)
        {
            GameOver();
        }
    }
}
