using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    [SerializeField] UnitMovementController unitMovement;
    [SerializeField] Unit unit;
    Animator animator;

    [SerializeField] float referenceSpeedMax = 6;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("MoveBlend", unitMovement.speed / referenceSpeedMax);
        animator.SetBool("IsMoving", unitMovement.IsMoving);
    }

    public void AnimateDance()
    {
        animator.SetTrigger("Dance");
    }

    public void AnimateFall()
    {
        animator.SetTrigger("Fall");
    }

    public void AnimateShoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void AnimateAnger()
    {
        animator.SetTrigger("Anger");
    }

    public void AnimateJump()
    {
        animator.SetTrigger("Jump");
    }
}