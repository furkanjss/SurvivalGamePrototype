using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
  private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = transform.parent. GetComponent<PlayerMovement>();
        if (animator == null)
        {
            Debug.LogError("Animator null");
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement null");
        }
    }

    private void OnEnable()
    {
        playerMovement.OnJump += HandleJump;
    }

    private void OnDisable()
    {
        playerMovement.OnJump -= HandleJump;
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("speed", playerMovement.SpeedPercent);
        animator.SetBool("IsRunning", playerMovement.IsRunning);
      
    }

    private void HandleJump()
    {
        animator.SetTrigger("IsJumping");
    }
}
