using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager: MonoBehaviour
{
    void MissionComplete()
    {
        Instance._missionSuccess.SetActive(true);
    }

    public void Continue()
    {
        _gameOver = false;
        Instance._missionSuccess.SetActive(false);
        Instance._difficulty.SetActive(true);
    }

    public void SuccessToMainMenu()
    {
        Instance._missionSuccess.SetActive(false);
        Instance._mainMenuUI.SetActive(true);
    }
}
