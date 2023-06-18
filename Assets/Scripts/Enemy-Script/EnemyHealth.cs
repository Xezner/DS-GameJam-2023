using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public Animator Animator;
    [SerializeField] public SpriteRenderer HealthSprite;
    public CardType CardType;
    public void TriggerHealth()
    {
        Animator.SetTrigger("Explode");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
