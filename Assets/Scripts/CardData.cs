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
    public Rigidbody2D Rigidbody;
    public Animator Animator;

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

            Rigidbody.MovePosition(Rigidbody.position + new Vector2(x * 1 * Time.fixedDeltaTime, 0f));
    }


}

public enum CardType
{
    None,
    Attack,
    Random,
    Medicard
}