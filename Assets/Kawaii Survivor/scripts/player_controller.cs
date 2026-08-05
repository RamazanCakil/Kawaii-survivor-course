using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_controller : MonoBehaviour
{
    [SerializeField] private MobileJoystick playerJoyistic;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        // Başlangıç hızı (isteğe bağlı)
        rig.linearVelocity = Vector2.right * moveSpeed;
    }

    private void FixedUpdate()
    {
        // Rigidbody2D için velocity saniyedeki hızdır, FixedUpdate içinde delta zamanı ile çarpılmaz
        rig.linearVelocity = playerJoyistic.GetMoveVector() * moveSpeed;
    }
}