using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager: MonoBehaviour
{
    void GameOver()
    {
        GameOverUI.SetActive(true);
    }
}
