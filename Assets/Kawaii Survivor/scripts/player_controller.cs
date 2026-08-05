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
        rig.linearVelocity = Vector2.right;
    }

    private void FixedUpdate()
    {
        rig.linearVelocity = playerJoyistic.GetMoveVector() * moveSpeed * Time.deltaTime;

    }
}