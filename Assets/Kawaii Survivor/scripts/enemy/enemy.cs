using System;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    protected EnemyMovement movement;

    [UnitHeaderInspectable("Health")]
    [SerializeField] protected int Maxhealth;
    protected int health;

    [Header("Elements")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    protected Player player;

    [Header("Spawn Sequence Releated")]
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] protected SpriteRenderer SpawnIndicator;
    protected bool hasSpawnd = false;
    [SerializeField] protected Collider2D collider;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius;


    [UnitHeaderInspectable("Actions")]
    public static Action<int, Vector2, bool > onDamageTaken;
    public static Action<Vector2> onPassedAway;


    [Header("Debug")]
    [SerializeField] protected bool gizmos;





    protected virtual void Start()
    {
        health = Maxhealth;
        player = FindObjectOfType<Player>();
        movement = GetComponent<EnemyMovement>();

        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            Destroy(gameObject);
        }

        startspawnSequence();

    }

    // Update is called once per frame
    protected bool CanAttack()
    {
        return renderer.enabled;
    }

    private void startspawnSequence()
    {
        setRendererVisibility(false);

        Vector3 targetScale = SpawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(SpawnIndicator.gameObject, targetScale, .3f).setLoopPingPong(4).setOnComplete(spawnSequenceComplete);
    }


    private void spawnSequenceComplete()
    {
        setRendererVisibility();
        // renderer.gameObject.SetActive(false);         bu kod tüm renderer'ı kapatır ve tüm bileşenleri devre dışı bırakır. Bu nedenle, sadece görünürlüğü kapatmak için enabled özelliğini kullanıyoruz.
        hasSpawnd = true;
        collider.enabled = true;

        movement.StorePlayer(player);

    }

    private void setRendererVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        SpawnIndicator.enabled = !visibility;
    }

    public void passAway()
    {

        onPassedAway?.Invoke(transform.position);

        PassAwayAfterWave();
    }

    public void PassAwayAfterWave()
    {
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();

        Destroy(gameObject);
    }

    public void takeDamage(int Damage,bool isCriticalHit)
    {
        int realDamage = Mathf.Min(Damage, health);
        health -= realDamage;

        onDamageTaken?.Invoke(Damage, transform.position,isCriticalHit);


        if (health <= 0)
        {
            passAway();
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (!gizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }

}
