using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void AnimateCameraDeath()
    {
        _animator.SetTrigger("Dead");
    }
}
