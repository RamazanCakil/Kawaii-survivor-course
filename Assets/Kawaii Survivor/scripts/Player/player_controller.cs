using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_controller : MonoBehaviour ,IPlayerStatsDependency      
{
    [SerializeField] private MobileJoystick playerJoyistic;
     private float moveSpeed;
    [SerializeField] private float baseMoveSpeed;
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

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedPercent = playerStatsManager.GetStatValue(stat.MoveSpeed)/100;
        moveSpeed = baseMoveSpeed * (1 + moveSpeedPercent);

    }
}