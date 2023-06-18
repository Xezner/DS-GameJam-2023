using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator Anim;
    public GameObject FadeToBlack;
    public GameObject MissionStart;
    public GameObject Tutorial;
    public void Click()
    {
        Tutorial.SetActive(false);
        MissionStart.SetActive(true);
        FadeToLevel();
    }

    public void FadeToLevel()
    {
        Anim.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        FadeToBlack.SetActive(false);
        SceneManager.LoadScene(GameManager.Scenes.GameScene.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(GameManager.Scenes.LoreScreen.ToString());
    }
}
