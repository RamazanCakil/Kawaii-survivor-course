using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement),typeof(RangeEnemyAttack))]

public class RangeEnemy : Enemy
{

    private RangeEnemyAttack attack;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {

        base.Start();
        attack = GetComponent<RangeEnemyAttack>();
       
        attack.StorePlayer(player);


    }


    void Update()
    {
        if (!CanAttack())
        {
            return;
        }

        ManageAttack();
    }
    private void ManageAttack() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {

            movement.FollowPlayer();
        }
        else {
            TryAttack();
        }

    }


    private void TryAttack()
    {
        attack.AutoAim();
        

    }

    
}

