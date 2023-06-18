using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public enum Scenes
    {
        //will change
        LoreScreen,
        UISample
    }
    public void Retry(int sceneNum)
    {
        SceneManager.LoadScene(Scenes.LoreScreen.ToString());
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Scenes.UISample.ToString());
    }
}
