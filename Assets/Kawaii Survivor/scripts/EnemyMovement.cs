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
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition=(Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
}