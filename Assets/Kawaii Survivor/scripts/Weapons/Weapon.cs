using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Boing : MonoBehaviour 
{
    [field : SerializeField] public WeaponDataSO weaponData { get; private set; }
    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Attack")]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int damage;
    [SerializeField] protected float AttackDelay;
    protected float AttackTimer;
    [SerializeField] protected Animator animator;

    [Header("Animation")]
    [SerializeField] protected float aimLerp;


    [Header("Critical")]
    protected int criticalChance;
    protected float criticalPercent;


    [Header("Level")]
    [field: SerializeField] public int Level {  get; private set; }

    protected virtual void Start()
    {
        damage = baseDamage; 
    }

    protected virtual void Update()
    {
    }

    // Bu metot artık 'enemy' scripti aramakla vakit kaybetmiyor, sadece hedefin yerini (Transform) buluyor.
    protected Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        float minDistance = range;

        if (enemies.Length <= 0)
            return null;

        for (int i = 0; i < enemies.Length; i++)
        {
            // Sadece mesafeyi ölçmek için objenin scriptini çekmeye gerek yoktur, direkt transform'u ölçebiliriz.
            float distanceToEnemy = Vector2.Distance(transform.position, enemies[i].transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemies[i].transform;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }


    protected int GetDamage(out bool IsCriticalHit) { 

    IsCriticalHit = false;
        if (Random.Range(0,101)<=criticalChance) {
            IsCriticalHit=true;
            return Mathf.RoundToInt(damage * criticalPercent);
        
        }
        return damage;
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }   

    /*
    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        damage = Mathf.RoundToInt(damage * (1+(weaponData.BaseStats[stat.Attack] / 100) + playerStatsManager.GetStatValue(stat.Attack)/100));

    }
    */
    protected void ConfigureStats()
    {
        float multiplier = 1 + (float)Level / 3;
        damage =Mathf.RoundToInt( weaponData.GetStatValue(stat.Attack) * multiplier);
        AttackDelay = 1f / (weaponData.GetStatValue(stat.AttackSpeed) * multiplier);

        criticalChance = Mathf.RoundToInt(weaponData.GetStatValue(stat.CriticalChance) * multiplier);
        criticalPercent = weaponData.GetStatValue(stat.CriticalPercent) * multiplier;

        //if(weaponData.Prefab.GetType() == typeof(rangeweapon))
           // range = weaponData.GetStatValue(stat.Range) * multiplier;

    }
}