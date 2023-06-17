using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _colliderSprite;
    [SerializeField] SpriteRenderer _playerArmSprite;
    [SerializeField] List<Sprite> _playerSpriteState;
    private InputType _inputType;
    private PlayerState _playerState;
    private Dictionary<PlayerState, Sprite> _playerSpriteStateDict;
    private void Start()
    {
        _playerSpriteStateDict = new()
        {
            { PlayerState.Idle, _playerSpriteState[0]},
            { PlayerState.Approving, _playerSpriteState[1]},
            { PlayerState.Rejecting, _playerSpriteState[2]},
            { PlayerState.Executing, _playerSpriteState[3]},
        };
    }

    private void Update()
    {
        //Prevents spamming of controlsd
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && _playerState != PlayerState.Idle)
        {
            return;
        }
        ResetAnimationTriggers();
        GetUserInput();
        UpdatePlayerState(_playerState);
    }

    void ResetAnimationTriggers()
    {
        _animator.ResetTrigger("Approve");
        _animator.ResetTrigger("Reject");
        _animator.ResetTrigger("Execute");
    }

    void GetUserInput()
    {
        _playerState = PlayerState.Idle;

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("APPROVE");
            _playerState = PlayerState.Approving;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("REJECT");
            _playerState = PlayerState.Rejecting;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("EXECUTE");
            _playerState = PlayerState.Executing;
        }
    }

    void UpdatePlayerState(PlayerState playerState)
    {
        _playerArmSprite.sprite = _playerSpriteStateDict[playerState];

        switch(playerState)
        {
            case PlayerState.Idle:
            {
                IdleState();
                break;
            }
            case PlayerState.Approving:
            {
                ApprovingState();
                break;
            }
            case PlayerState.Rejecting:
            {
                RejectingState();
                break;
            }
            case PlayerState.Executing:
            {
                ExecutingState();
                break;
            }
            default: break;
        }
    }

    void IdleState()
    {

    }

    void ApprovingState()
    {
        _inputType = InputType.Approve;
        _colliderSprite.color = Color.cyan;
        _animator.SetTrigger("Approve");
    }

    void RejectingState()
    {
        _inputType = InputType.Reject;
        _colliderSprite.color = Color.magenta;
        _animator.SetTrigger("Reject");
    }

    void ExecutingState()
    {
        _colliderSprite.color = Color.gray;
        _animator.SetTrigger("Execute");
    }

    public void AnimateExecution()
    {
        AttackSequenceManager.Instance.ExecuteAttack();
    }

    public void AssignMove(Collider2D collision)
    {
        var proceduralObject = collision.transform.GetComponentInParent<CardData>();
        proceduralObject.InputType = _inputType;
        proceduralObject.IsTouched = true;
        var cardType = proceduralObject.CardType;
        Debug.Log($"CARD, {cardType}");
    }
}

public enum InputType
{
    None,
    Approve,
    Reject,
    Execute
}

public enum PlayerState
{
    Idle,
    Approving,
    Rejecting,
    Executing
}
