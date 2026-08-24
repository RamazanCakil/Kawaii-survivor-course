using UnityEngine; // DÜZELTME 1: "using System;" kütüphanesi silindi. Artık Random hata vermeyecek.
using UnityEngine.Pool;
public class DropsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private candy candyPrefab;
    [SerializeField] private cash cashPrefab;
    [SerializeField] private chest chestPrefab;

    [Header("Settings")]
    [SerializeField] [Range(0,100)]  private int cashDropChance;
    [SerializeField] [Range(0, 100)] private int chestDropChance;




    [Header("Pooling")]
    private ObjectPool<candy> candyPool;
    private ObjectPool<cash> cashPool;

    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
        candy.onCollected += ReleaseCandy;
        cash.onCollected += ReleaseCash;
    }
    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassedAwayCallBack;
        candy.onCollected -= ReleaseCandy;
        cash.onCollected -= ReleaseCash;
    }
    private void Start()
    {
        candyPool = new ObjectPool<candy>(CandyCreateFunction, CandyActionOnGet, CandyActionOnRelease, CandyActionOnDestroy);
        cashPool = new ObjectPool<cash>(CashCreateFunction, CashActionOnGet, CashActionOnRelease, CashActionOnDestroy);

    }
    private void Update()
    {
        
    }

    private candy CandyCreateFunction()             => Instantiate(candyPrefab,transform);
    private void CandyActionOnGet(candy candy)      => candy.gameObject.SetActive(true);
    private void CandyActionOnRelease(candy candy)  => candy.gameObject.SetActive(false);
    private void CandyActionOnDestroy(candy candy)  => Destroy(candy.gameObject);

    private cash CashCreateFunction()             => Instantiate(cashPrefab, transform);
    private void CashActionOnGet(cash cash)      => cash.gameObject.SetActive(true);
    private void CashActionOnRelease(cash cash)  => cash.gameObject.SetActive(false);
    private void CashActionOnDestroy(cash cash)  => Destroy(cash.gameObject);



    private void EnemyPassedAwayCallBack(Vector2 EnemyPosition)
    {
        // %20 ihtimalle para, %80 ihtimalle şeker düşürme hesabı
        bool shouldSpawnCash = Random.Range(0, 101) <= cashDropChance;

        DroppableCurrency droppable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();
        droppable.transform.position = EnemyPosition;
        tryDropChest(EnemyPosition);
    }

    private void tryDropChest(Vector2 spawnPosition) {

        bool shouldSpawnCash = Random.Range(0, 101) <= chestDropChance;
        if (!shouldSpawnCash)
            return;
        Instantiate(cashPrefab, spawnPosition,Quaternion.identity,transform);
    }

    private void ReleaseCandy(candy candy) => candyPool.Release(candy);
    private void ReleaseCash(cash cash) => cashPool.Release(cash);
}