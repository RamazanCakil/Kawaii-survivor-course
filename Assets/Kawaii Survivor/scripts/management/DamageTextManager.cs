using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;



    private void Awake()
    {
        MeleeEnemy.onDamageTaken += EnemyHitCallBack;

        player_healt.onAttackDodged += AttackDodgeCallBack;
    }

    private void OnDestroy()
    {
        player_healt.onAttackDodged -= AttackDodgeCallBack;

        MeleeEnemy.onDamageTaken -= EnemyHitCallBack;
    }




    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        DamageText instance = Instantiate(damageTextPrefab, transform);
        // Üretilen yazıya "Senin iade edileceğin havuz bu" diyoruz
        instance.SetPool(damageTextPool);
        return instance;
    }

    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    private void EnemyHitCallBack(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
        Vector3 randomSpawnPosition = enemyPos + Vector2.up * 1.5f;

        DamageText damageTextInstance = damageTextPool.Get();
        damageTextInstance.transform.position = randomSpawnPosition;

        // Yazı animasyonunu başlat (Geri sayım ve havuza iade işini artık yazının kendi scripti halledecek)
        damageTextInstance.DamageAnimation(damage.ToString(), isCriticalHit);
    }

    private void AttackDodgeCallBack(Vector2 playerPosition)
    {
        Vector3 randomSpawnPosition = playerPosition + Vector2.up * 1.5f;

        DamageText damageTextInstance = damageTextPool.Get();
        damageTextInstance.transform.position = randomSpawnPosition;

        damageTextInstance.DamageAnimation("Dodged", false);


    }

}