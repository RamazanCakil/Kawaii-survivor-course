using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [UnitHeaderInspectable("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [UnitHeaderInspectable("Pooling")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        enemy.onDamageTaken += EnemyHitCallBack;
    }
    private void OnDestroy()
    {
        enemy.onDamageTaken -= EnemyHitCallBack;
    }

    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    // Üretim metodu: Geriye obje döndürmesi gerektiği için DamageText kalır
    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    // Get metodu: Sadece görünür yapacağı için void olmalıdır
    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    // Release metodu: Sadece görünmez yapacağı için void olmalıdır
    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    // Destroy metodu: Sadece sileceği için void olmalıdır
    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    private void EnemyHitCallBack(int damage, Vector2 enemyPos)
    {
        Vector3 randomSpawnPosition = enemyPos + Vector2.up * 1.5f;

        DamageText damageTextInstances = damageTextPool.Get();
        //DamageText damageTextInstances = Instantiate(damageTextPrefab, randomSpawnPosition ,Quaternion.identity, transform);

        damageTextInstances.transform.position = randomSpawnPosition;

        damageTextInstances.DamageAnimation(damage);

        LeanTween.delayedCall(1f, () => damageTextPool.Release(damageTextInstances));

    }

}
