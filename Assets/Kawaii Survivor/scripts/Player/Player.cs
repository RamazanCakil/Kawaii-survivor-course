using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(player_healt),typeof(playerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [UnitHeaderInspectable("Components")]
    [SerializeField] private CircleCollider2D collider;
    private player_healt playerHealth;
    private playerLevel playerLevel;

    // Start, script aktif olduğunda ilk karede (frame) bir kez çalışır

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerHealth = GetComponent<player_healt>();
        playerLevel = GetComponent<playerLevel>();
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

    public bool hasLeveledUp() {

        return playerLevel.hasLeveledUp();
    }

}