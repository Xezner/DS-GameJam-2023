using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoreScreen : MonoBehaviour
{
    [SerializeField] private string[] _dialogueStrings;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _clickText;
    [SerializeField] private GameObject _loreScreen;
    [SerializeField] private int _index;
    [SerializeField] private float _wordSpeed;

    private void Start()
    {
        StartCoroutine(TypingEffect());
    }

    public void NextLine()
    {
        if (_index < _dialogueStrings.Length -1)
        {
            _index++;
            _dialogueText.text = "";
            StartCoroutine(TypingEffect());
        }
        else
        {
            Destroy(_loreScreen);
        }
    }





    IEnumerator TypingEffect()
    {
        foreach(char letter in _dialogueStrings[_index].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_wordSpeed);
        }
    }
}
