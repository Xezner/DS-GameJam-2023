using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoreScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private float _scrollSpeed;
    public GameObject Tutorial;
    public GameObject ScreenButton;
    public GameObject DialogueText;
    public Transform DialogueTextTrans;
    private int camPos = 2000;

    private void Start()
    {

    }

    void TextCrawl()
    {
        transform.Translate(Camera.main.transform.up * _scrollSpeed * Time.deltaTime);
    }

    private void Update()
    {
        //TextCrawl();

        //if (DialogueTextTrans.position.y > camPos)
        //{
        //    ShowTutorial();
        //}
    }

    void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void ClickAnywhereToContinue()
    {
        Debug.LogError("TEST");
        DialogueText.SetActive(false);
        ShowTutorial();
    }
}
