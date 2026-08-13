using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(player_healt))]
public class Player : MonoBehaviour
{
    [UnitHeaderInspectable("Components")]
    [SerializeField] private CircleCollider2D collider;
    private player_healt playerHealth;

    // Start, script aktif olduğunda ilk karede (frame) bir kez çalışır

    private void Awake()
    {
        playerHealth = GetComponent<player_healt>();
    }

    void Start()
    {
           
    }

    // Update, oyun çalıştığı sürece her karede (frame) sürekli olarak çalışır
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamagee(damage);

    }
    public Vector2 GetCenter() { 
    
            return (Vector2)transform.position + collider.offset;
    }

}