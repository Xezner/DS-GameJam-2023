using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    public Animator Anim;
    public GameObject FadeToBlack;
    public GameObject MissionStart;
    public GameObject Tutorial;
    public bool IsDialogueStarting = true;

    private void Start()
    {
        IsDialogueStarting = true;
    }

    public void Click()
    {
        Debug.Log("CLICKED");
        if (IsDialogueStarting)
        {
            Anim.ResetTrigger("Dialogue");
            Anim.StopPlayback();
            _dialogueText.gameObject.SetActive(false);
            ShowTutorial();
        }
        else
        {
            Tutorial.SetActive(false);
            SceneManager.LoadScene(Scenes.GameSceneMain.ToString(), LoadSceneMode.Additive);
            //Load CardMechanicManager
            //MissionStart.SetActive(true);
        }
    }

    public void TriggerScroll()
    {
        Anim.SetTrigger("Dialogue");
    }

    public void ShowTutorial()
    {
        IsDialogueStarting = false;
        FadeToBlack.SetActive(true);
        Tutorial.SetActive(true);
    }

    public void FadeToLevel()
    {
        Anim.SetTrigger("FadeOut");
    }


}
