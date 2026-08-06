using Unity.VisualScripting;
using UnityEngine;

public class Boing : MonoBehaviour
{
    [UnitHeaderInspectable("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [UnitHeaderInspectable("Animation")]
    [SerializeField] private float aimLerp;




    void Start()
    {
        
    }
    void Update()
    {
        autoAim();


    }

    private void autoAim()
    {
        enemy closestEnemy=GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;
        if (closestEnemy != null) 
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;

        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);

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

    }

}
