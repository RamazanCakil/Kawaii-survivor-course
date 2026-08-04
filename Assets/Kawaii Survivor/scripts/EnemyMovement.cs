/*
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    // Start, script aktif olduğunda ilk karede (frame) bir kez çalışır
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogError("Player script not found in the scene.");
            Destroy(gameObject);

        }
    }

    // Update, oyun çalıştığı sürece her karede (frame) sürekli olarak çalışır
    void Update()
    {
        FollowPlayer();
        TryAttack();

    }
    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition=(Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
    private void TryAttack() { 
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius ) { 
            Destroy(gameObject);
        
        }
    
    
    }
}

*/
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    // 1. DEĞİŞİKLİK: Player yerine player_controller yazıyoruz
    private player_controller player;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Spawn Sequence Releated")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer SpawnIndicator;



    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    [Header("Debug")]
    [SerializeField] private bool gizmos;


    void Start()
    {
        // 2. DEĞİŞİKLİK: Sahnedeki player_controller'ı bulmasını söylüyoruz
        player = FindAnyObjectByType<player_controller>();

        if (player == null)
        {
            Debug.LogError("Player script not found in the scene.");
            Destroy(gameObject);
        }

        renderer.enabled = false;
        // renderer.gameObject.SetActive(false);         bu kod tüm renderer'ı kapatır ve tüm bileşenleri devre dışı bırakır. Bu nedenle, sadece görünürlüğü kapatmak için enabled özelliğini kullanıyoruz.
        SpawnIndicator.enabled = true;



    }

    void Update()
    {
        if (player == null) {return;
    }
        FollowPlayer();
        TryAttack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            passAway();
        }
    }
    private void passAway()
    {
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        if (!gizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }
}