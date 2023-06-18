using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public GameObject ObjectPrefab;
    public CardType CardType = CardType.None;
    public SpriteRenderer Card;
    public SpriteRenderer Overlay;
    public SpriteRenderer Icon;
    public InputType InputType = InputType.None;
    public bool IsTouched = false;
    public Rigidbody2D Rigidbody;
    public Animator Animator;
    public Collider2D Collider;

    private Color _transparent = new Color(1, 1, 1, 0);

    public void HideSprites()
    {
        Card.gameObject.SetActive(false);
        Overlay.gameObject.SetActive(false);
        Icon.gameObject.SetActive(false);
    }
    public void ShowSprites()
    {
        Card.gameObject.SetActive(true);
        Overlay.gameObject.SetActive(true);
        Icon.gameObject.SetActive(true);
    }
}



public enum CardType
{
    None,
    Attack,
    Random,
    Medicard
}