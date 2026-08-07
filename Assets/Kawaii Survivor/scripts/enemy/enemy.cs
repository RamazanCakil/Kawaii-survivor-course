
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

[RequireComponent(typeof(EnemyMovement))]
public class enemy : MonoBehaviour
{


    [Header("Elements")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    private Player player;

    [Header("Components")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    private EnemyMovement movement;

    [Header("Spawn Sequence Releated")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer SpawnIndicator;
    private bool hasSpawnd = false;
    [SerializeField] private Collider2D collider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;


    [Header("Attack Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;
     

    [UnitHeaderInspectable("Health")]
    [SerializeField] private int Maxhealth;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [UnitHeaderInspectable("Actions")]
    public static Action<int,Vector2> onDamageTaken;

    [Header("Debug")]
    [SerializeField] private bool gizmos;



    void Start()
    {

        health = Maxhealth;
        healthText.text = health.ToString();
        player = FindObjectOfType<Player>();
        movement =GetComponent<EnemyMovement>();

        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            Destroy(gameObject);
        }

        attackDelay = 1f / attackFrequency;
        startspawnSequence();

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

    private void setRendererVisibility(bool visibility=true)
    {
        renderer.enabled = visibility;
        SpawnIndicator.enabled = !visibility;
    }

    void Update()
    {


        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else { Wait(); }
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }
    }

    private void Attack()
    {
        attackTimer = 0;
        player.TakeDamage(damage);
    }

    private void passAway()
    {
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();

        Destroy(gameObject);
    }

    public void takeDamage(int Damage) 
    {
        int realDamage = Mathf.Min(Damage, health);
        health -= realDamage;
        
        onDamageTaken?.Invoke(Damage,transform.position);

        healthText.text = health.ToString();

        if (health<=0)
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


