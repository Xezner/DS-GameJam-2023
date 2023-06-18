using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public enum Scenes
    {
        //will change
        LoreScene,
        UISample
    }
    public void Retry(int sceneNum)
    {
        SceneManager.LoadScene(Scenes.LoreScene.ToString());
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Scenes.UISample.ToString());
    }
}
