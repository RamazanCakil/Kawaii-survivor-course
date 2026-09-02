using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Boing , IPlayerStatsDependency
{
    enum State
    {
        idle,
        Attack
    }

    // ÇÖZÜM 1: Eksik olan değişkeni buraya ekledik.
    private State state;

    [Header("Elements")]
    [SerializeField] private Transform HitDetectionTransform;
    [SerializeField] private BoxCollider2D hitCollider;
    private List<Collider2D> damagedEnemy = new List<Collider2D>();


    // ÇÖZÜM 2: Ana koddaki Start'ı "override" ile ezdik.
    protected override void Start()
    {
        base.Start();
        state = State.idle;
    }

    // ÇÖZÜM 3: Ana koddaki Update'i "override" ile ezdik.
    protected override void Update()
    {
        base.Update();

        switch (state)
        {
            case State.idle:
                autoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }
    }

    private void autoAim()
    {
        Transform closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;
        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        incramentAttackTimer();
    }

    private void ManageAttack()
    {
        if (AttackTimer >= AttackDelay)
        {
            AttackTimer = 0;
            StartAttack();
        }
    }

    private void incramentAttackTimer()
    {
        AttackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(
            HitDetectionTransform.position,
            hitCollider.bounds.size,
            HitDetectionTransform.localEulerAngles.z,
            enemyMask
        );

        for (int i = 0; i < enemies.Length; i++)
        {
            Collider2D hitObj = enemies[i];

            if (!damagedEnemy.Contains(hitObj))
            {
                if (hitObj.TryGetComponent(out Enemy normalEnemy))
                {
                    int damage = GetDamage(out bool IsCriticalHit);

                    normalEnemy.takeDamage(damage,IsCriticalHit);
                    damagedEnemy.Add(hitObj);
                }
                else if (hitObj.TryGetComponent(out RangeEnemy rangeEnemy))
                {
                    int damage = GetDamage(out bool IsCriticalHit);

                    rangeEnemy.takeDamage(damage, IsCriticalHit);
                    damagedEnemy.Add(hitObj);
                }
            }
        }
    }

    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        damagedEnemy.Clear();

        animator.speed = 1f / AttackDelay;

        // Silahın saldırı durumunda sonsuza kadar takılı kalmasını engeller.
        LeanTween.delayedCall(gameObject, 0.3f, StopAttack);
    }

    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.idle;
        damagedEnemy.Clear();
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        damage = Mathf.RoundToInt(damage * (1 + (weaponData.BaseStats[stat.Attack] / 100) + playerStatsManager.GetStatValue(stat.Attack) / 100));
        AttackDelay /= 1 + (playerStatsManager.GetStatValue(stat.AttackSpeed) / 100);

        criticalChance = Mathf.RoundToInt(criticalChance * (1 + playerStatsManager.GetStatValue(stat.CriticalChance) / 100));
        criticalPercent += playerStatsManager.GetStatValue(stat.CriticalPercent);

    }

}