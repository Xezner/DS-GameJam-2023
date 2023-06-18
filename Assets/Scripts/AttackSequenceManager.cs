using System.Collections;
using System.Collections.Generic;
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
                    AddAttackSequence(cardData.CardType, cardData.SpriteRenderer.color);
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
            _attackSequenceAnimationController.ExecuteAttack();
        }
    }

    public void ResetAttackSequence()
    {
        foreach(SpriteRenderer attackSprite in AttackSprites)
        {
            attackSprite.color = Color.white;
        }
        _attackCardSequence.Clear();
        _attackCount = 0;
    }

    public void AddAttackSequence(CardType cardType, Color colorStyle)
    {
        if(_attackCount == MAX_ATTACK_COUNT)
        {
            return;
        }
        AttackSprites[_attackCount].color = colorStyle;
        _attackCardSequence.Add(cardType);
        _attackCount++;
    }

    public List<CardType> GetAttackSequence()
    {
        return _attackCardSequence;
    }
}
