using UnityEngine;
using UnityEngine.Pool;

public class rangeweapon : Boing , IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private Transform shootingpoint; // Küçük harfle
    [SerializeField] private Bullet BulletPrefab;

    [Header("Pool")]
    private ObjectPool<Bullet> bulletPool;

    // KALITIM DÜZELTMESİ: Start metodunu override ettik
    protected override void Start()
    {
        base.Start(); // Ana koddaki Start'ı da çalıştırır
        bulletPool = new ObjectPool<Bullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    // KALITIM DÜZELTMESİ: Update metodunu override ettik
    protected override void Update()
    {
        base.Update();
        autoAim();
    }

    // ÇÖZÜM: Merminin çağırıp hata verdiği o eksik metot eklendi!
    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    // HAVUZ (POOL) METOTLARI DÜZELTİLDİ
    private Bullet CreateFunction()
    {
        // EnemyBullet değil, senin kendi mermin (Bullet) oluşturuluyor
        Bullet bulletInstance = Instantiate(BulletPrefab, shootingpoint.position, Quaternion.identity);
        return bulletInstance;
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload(); // Mermiyi sıfırla
        bullet.transform.position = shootingpoint.position;
        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }


    private void autoAim()
    {
        Transform closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.position - transform.position).normalized;

            // DİKKAT: Burada transform.up = targetUpVector; vardı ve Lerp (yumuşak dönme) kodunu bozuyordu, o yüzden sildim!
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);

            ManageShooting();
            return;
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }

    private void ManageShooting()
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer >= AttackDelay)
        {
            AttackTimer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {

        int damage = GetDamage(out bool IsCriticalHit);

        Bullet bulletInstance = bulletPool.Get();
        // SİLAHI (this) MERMİYE GÖNDERİYORUZ
        bulletInstance.Shoot(damage, transform.up, this, IsCriticalHit);
    }


    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        damage = Mathf.RoundToInt(damage * (1 + (weaponData.BaseStats[stat.Attack] / 100) + playerStatsManager.GetStatValue(stat.Attack) / 100));
        AttackDelay /= 1 + (playerStatsManager.GetStatValue(stat.AttackSpeed) / 100);

        criticalChance = Mathf.RoundToInt(criticalChance * (1 + playerStatsManager.GetStatValue(stat.CriticalChance) / 100));
        criticalPercent +=  playerStatsManager.GetStatValue(stat.CriticalPercent);
        range += playerStatsManager.GetStatValue(stat.Range) / 10 ;
    }

}