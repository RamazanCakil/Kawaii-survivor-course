using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [Header("Elements")]
    private Player player;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;



    [Header("Attack Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header("Attack Settings")]
    private ObjectPool<EnemyBullet> bulletPool;


    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay; 
        bulletPool=new ObjectPool<EnemyBullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);

    }

    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab,shootingPoint.position,Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
        
    }

    // Get metodu: Sadece görünür yapacağı için void olmalıdır
    private void ActionOnGet(EnemyBullet bullet)
    {
        bullet.reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }

    // Release metodu: Sadece görünmez yapacağı için void olmalıdır
    private void ActionOnRelease(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Destroy metodu: Sadece sileceği için void olmalıdır
    private void ActionOnDestroy(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }




    public void ReleaseBullet(EnemyBullet bullet)
    {
        bulletPool.Release(bullet);
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    public void AutoAim() 
    {
        ManageShooting();

    }
    private void ManageShooting() { 
    
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay) 
        { 
            attackTimer = 0f;
            Shoot();
        }
    }

    private void Shoot() {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;

        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.shoot(damage,direction);
        
    }
    
}
