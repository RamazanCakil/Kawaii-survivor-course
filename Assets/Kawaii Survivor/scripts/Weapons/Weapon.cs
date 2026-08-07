using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boing : MonoBehaviour
{

    enum State
    {
        idle,
        Attack
    }
    private State state;

    [UnitHeaderInspectable("Elements")]
    [SerializeField] private Transform HitDetectionTransform;
    [SerializeField] private float HitDetectionRadius;
    [SerializeField] private BoxCollider2D hitCollider;


    [UnitHeaderInspectable("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [UnitHeaderInspectable("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float AttackDelay;
    private float AttackTimer;
    [SerializeField] private Animator animator;
    private List<enemy> damagedEnemy = new List<enemy>();


    [UnitHeaderInspectable("Animation")]
    [SerializeField] private float aimLerp;




    void Start()
    {
        state = State.idle;
    }
    void Update()
    {

        switch(state)
        {
            case State.idle:
                autoAim();
                break;
            case State.Attack: Attacking();
                break;
        }

        

    }

    private void Attack() 
    {

        //Collider2D[] enemies = Physics2D.OverlapCircleAll(HitDetectionTransform.position, HitDetectionRadius, enemyMask);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(HitDetectionTransform.position,
            hitCollider.bounds.size,
            HitDetectionTransform.localEulerAngles.z,
            enemyMask );
        for (int i = 0; i < enemies.Length; i++) 
        {
            enemy enemyy = enemies[i].GetComponent<enemy>();

            if (!damagedEnemy.Contains(enemyy)) {
                enemyy.takeDamage(damage);
                damagedEnemy.Add(enemyy);
            }


            //enemies[i].GetComponent<enemy>().takeDamage(damage);
        }

    }
    private void StartAttack() {

        animator.Play("Attack");
        state = State.Attack;
        damagedEnemy.Clear();

        animator.speed = 1f / AttackDelay;

    }
    private void Attacking() {

        Attack();
    
    }
    private void StopAttack()
    {
        state = State.idle;
        damagedEnemy.Clear();
    }

    private void autoAim()
    {
        enemy closestEnemy=GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;
        if (closestEnemy != null) 
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();


        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        incramentAttackTimer();
    }

    private void ManageAttack() {
        if (AttackTimer >= AttackDelay) {
            AttackTimer = 0;
            StartAttack();

        }
    }
    private void incramentAttackTimer() { 
    
        AttackTimer += Time.deltaTime;
    }
    private enemy GetClosestEnemy()
    {

        enemy closestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        float minDistance = range;


        if (enemies.Length <= 0)
            return null;

        for (int i = 0; i < enemies.Length; i++)
        {
            enemy enemyChecked = enemies[i].GetComponent<enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;


    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitDetectionTransform.position, HitDetectionRadius);

    }

}
