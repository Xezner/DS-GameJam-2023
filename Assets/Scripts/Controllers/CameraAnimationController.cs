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

    public void AnimateCameraWin()
    {
        _animator.SetTrigger("Win");
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public void OpenGameOverScreen()
    {
        CardMechanicManager.Instance.OpenGameOverScreen();
    }
}
