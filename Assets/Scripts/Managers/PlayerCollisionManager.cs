using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private PlayerArmController _playerArmController;
    [SerializeField] Collider2D _playerCollider;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            Debug.Log("TOUCH");
            _playerArmController.AssignMove(collision);
        }
    }
}
