using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunCollisionManager : MonoBehaviour
{
    public float damage;
    private bool canAttack = false;
    private PlayerCombatController playerCombatController;

    private void Start()
    {
        if (playerCombatController == null)
        {
            playerCombatController = GameObject.FindWithTag("Player").GetComponent<PlayerCombatController>();
        }
        playerCombatController.OnAttackTriggered += AttackOpen;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
                canAttack = false; 
            }
        }
    }

    private void AttackOpen()
    {
        if (!canAttack) 
        {
            canAttack = true;
            DOVirtual.DelayedCall(2, () =>
            {
                canAttack = false;
            });
        }
    }


    private void OnDisable()
    {
        playerCombatController.OnAttackTriggered -= AttackOpen;
    }
}
