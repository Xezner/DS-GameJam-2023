using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _gameOver = true;

    [SerializeField] private GameObject _difficulty;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _gameOverUI;
    public GameObject Difficulty { get { return _difficulty; } set { value = _difficulty; } }
    public GameObject MainMenuUI { get { return _mainMenuUI; } set { value = _mainMenuUI; } }
    public GameObject GameOverUI { get { return _gameOverUI; } set { value = _gameOverUI; } }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_gameOver)
        {
            GameOver();
        }
    }
}
