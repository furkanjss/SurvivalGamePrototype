using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ZombieEnemy : BaseEnemy
{
    public GameObject dropObject;
    private Transform target;
    private float distanceToPlayer;
    public float chaseDistance = 10f; // Kovalama mesafesi
    public float attackDistance = 2f; // Saldırma mesafesi
    private void Start()
    {
        Initialize(health,speed,damage);
         target = GameObject.Find("EnemyTargetPosition").transform;
      

    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, target.position);

        if (distanceToPlayer < chaseDistance)
        {
            if (distanceToPlayer < attackDistance)
            {
              Attack();
            }
            else
            {
                Move(target);
            }
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
    public override void Initialize(float health, float speed, int damage)
    {
        base.Initialize(health, speed, damage);
        Debug.Log("ZombieEnemy initialized.");
    }


    public override void Attack()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
 
    }
    public override void Die()
    {
        agent.enabled = false;
        this.enabled = false;
        anim.Play("Die");
      GameObject tempDropObject=  Instantiate(dropObject);
      tempDropObject.transform.position = new Vector3(transform.position.x,transform.position.y+.2f,transform.position.z);
      tempDropObject.transform.parent = null;
      transform.DOScale(0, 1).SetDelay(1).OnComplete(() =>
      {
          Destroy(gameObject);
      });
    }

    // Zombi hareket davranışı
    public override void Move(Transform target)
    {
        base.Move(target);
        agent.isStopped = false;
        agent.SetDestination(target.position);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<SurvivalManager>().LoseHealth(damage*Time.deltaTime);
        }
    }  
}
