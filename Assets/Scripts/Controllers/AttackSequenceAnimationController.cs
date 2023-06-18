using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSequenceAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<SpriteRenderer> _attackSprites;
    public void ExecuteAttack()
    {
        for (int i = 0; i < _attackSprites.Count; i++)
        {
            var sprite = _attackSprites[i];
            sprite.sprite = AttackSequenceManager.Instance.AttackSprites[i].sprite;
            sprite.color = AttackSequenceManager.Instance.AttackSprites[i].color;
        }
        AttackSequenceManager.Instance.ResetAttackSequence();
        _animator.SetTrigger("Execute");
    }
}
