using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public GameObject ObjectPrefab;
    public CardType CardType = CardType.None;
    public SpriteRenderer SpriteRenderer;
    public InputType InputType = InputType.None;
    public bool IsTouched = false;
}

public enum CardType
{
    None,
    TypeOne,
    TypeTwo,
    TypeThree
}