
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{
    [Header("Attack Settings")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackFrequency;
    protected float attackDelay;
    protected float attackTimer;


    protected override void Start()
    {
        base.Start();
        attackDelay = 1f / attackFrequency;

    }


    void Update()
    {
        if (!CanAttack())
        {
            return;
        }


        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else { Wait(); }

        movement.FollowPlayer();
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }
        
        
    }

    private void Attack()
    {
        attackTimer = 0;
        
        player.TakeDamage(damage);
        
    }

}


