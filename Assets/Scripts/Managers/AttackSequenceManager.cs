using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackSequenceManager : MonoBehaviour
{
    [SerializeField] private Transform[] _attackSequenceSpawnPoints;
    [SerializeField] public List<SpriteRenderer> AttackSprites;
    [SerializeField] private List<CardType> _attackCardSequence;
    [SerializeField] public Animator Animator;
    [SerializeField] private AttackSequenceAnimationController _attackSequenceAnimationController;   
    private int _attackCount;
    const int MAX_ATTACK_COUNT = 3;

    public static AttackSequenceManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitiateAttackSequence(CardData cardData)
    {
        switch (cardData.InputType)
        {
            case InputType.Approve:
                {
                    AddAttackSequence(cardData.CardType, cardData.Icon.sprite);
                    break;
                }
            case InputType.Reject:
                {
                    break;
                }
            case InputType.None:
                {
                    Instance.ResetAttackSequence();
                    break;
                }
            default: break;
        }
    }

    public void ExecuteAttack()
    {
        if ( (_attackCardSequence.Count >= 1 && _attackCardSequence.Contains(CardType.Medicard)) || _attackCardSequence.Count == 3)
        {
            int timeIncreaseCounter = 0;
            Debug.Log($"HERE : {_attackCardSequence[0]}");
            foreach(var card in _attackCardSequence)
            {
                Debug.Log($"CARD ATTACK: {card}");
                if(card == CardType.Medicard)
                {
                    timeIncreaseCounter++;
                    continue;
                }
                if (EnemyManager.Instance.EnemyHealthList.Count != 0)
                {
                    EnemyManager.Instance.AttackCheck(card);
                }
            }
            TimeManager.Instance.IncreaseTime(timeIncreaseCounter);
            _attackSequenceAnimationController.ExecuteAttack();

        }
    }

    

    public void ResetAttackSequence()
    {
        foreach(SpriteRenderer attackSprite in AttackSprites)
        {
            attackSprite.sprite = null;
        }
        _attackCardSequence.Clear();
        _attackCount = 0;
    }

    public void AddAttackSequence(CardType cardType, Sprite cardStyle)
    {
        if(_attackCount == MAX_ATTACK_COUNT)
        {
            return;
        }
        AttackSprites[_attackCount].sprite = cardStyle;
        _attackCardSequence.Add(cardType);
        _attackCount++;
    }

    public List<CardType> GetAttackSequence()
    {
        return _attackCardSequence;
    }
}
