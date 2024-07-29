using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour,IDamagable
{
    public float health;
    public float speed;
    public int damage;
    public NavMeshAgent agent;
    public Animator anim;
    public virtual void Initialize(float health, float speed, int damage)
    {
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        agent.speed = this.speed;
    }


    public virtual void Attack()
    {
        Debug.Log("BaseEnemy attacks with " + damage + " damage.");
    }
    public virtual void Move(Transform target)
    {
        agent.SetDestination(target.transform.position);
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

 
    public virtual  void Die()
    {
        
    }
}
