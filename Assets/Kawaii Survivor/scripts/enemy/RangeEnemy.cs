using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement),typeof(RangeEnemyAttack))]

public class RangeEnemy : MonoBehaviour
{
    [Header("Elements")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    private Player player;

    [Header("Components")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    private EnemyMovement movement;
    private RangeEnemyAttack attack;

    [Header("Spawn Sequence Releated")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer SpawnIndicator;
    private bool hasSpawnd = false;
    [SerializeField] private Collider2D collider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;


    [Header("Attack Settings")]
    [SerializeField] private float playerDetectionRadius;



    [UnitHeaderInspectable("Health")]
    [SerializeField] private int Maxhealth;
    private int health;

    [UnitHeaderInspectable("Actions")]
    public static Action<int, Vector2> onDamageTaken;

    [Header("Debug")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        attack = GetComponent<RangeEnemyAttack>();

        health = Maxhealth;
        player = FindObjectOfType<Player>();
        movement = GetComponent<EnemyMovement>();

        attack.StorePlayer(player);


        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            Destroy(gameObject);
        }

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

    private void setRendererVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        SpawnIndicator.enabled = !visibility;
    }

    // Update is called once per frame
    void Update()
    {

        if (!renderer.enabled) {
            return;
        }


        ManageAttack();
    }
    private void ManageAttack() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {

            movement.FollowPlayer();
        }
        else {
            TryAttack();
        }

    }

    


    private void TryAttack()
    {
        attack.AutoAim();
        

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

        onDamageTaken?.Invoke(Damage, transform.position);


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

