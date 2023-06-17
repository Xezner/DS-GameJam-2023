using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    [SerializeField] Animator _animator;

    InputType InputType;
    private void Update()
    {
        _animator.ResetTrigger("Approve");
        
        if (Input.GetMouseButtonDown(1))
        {
            InputType = InputType.Approve;
            _animator.SetTrigger("Approve");
        }
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("ERROR");
            AttackSequenceManager.Instance.ExecuteAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Card"))
        {
            var proceduralObject = collision.transform.GetComponentInParent<CardData>();
            proceduralObject.InputType = InputType;
            proceduralObject.IsTouched = true;
            var cardType = proceduralObject.CardType;
            Debug.Log($"CARD, {cardType}");
        }
    }
}

public enum InputType
{
    None,
    Approve,
    Reject,
    Execute
}
