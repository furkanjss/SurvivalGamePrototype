using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerCombatController _combatController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _playerController = transform.parent. GetComponent<PlayerController>();
        _combatController = transform.parent. GetComponent<PlayerCombatController>();
        if (animator == null)
        {
            Debug.LogError("Animator null");
        }

        if (_playerController == null)
        {
            Debug.LogError("PlayerController null");
        }
    }

    private void OnEnable()
    {
        _playerController.OnJump += HandleJump;
        _combatController.OnAttackTriggered += HandleAttack;
        GameManager.OnPlayerDie += DieAnimation;
    }

    private void OnDisable()
    {
        _playerController.OnJump -= HandleJump;
        _combatController.OnAttackTriggered -= HandleAttack;
        GameManager.OnPlayerDie -= DieAnimation;

    }

    private void FixedUpdate()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsRunning", _playerController.IsRunning);
        animator.SetFloat("horizontal",_playerController.horizontalInput);
        animator.SetFloat("vertical",_playerController.verticalInput);
    }

    private void HandleJump()
    {
        animator.SetTrigger("IsJumping");
    }   private void HandleAttack()
    {
        animator.Play("Attack");
    } private void DieAnimation()
    {
        animator.Play("Die");
    }
}
