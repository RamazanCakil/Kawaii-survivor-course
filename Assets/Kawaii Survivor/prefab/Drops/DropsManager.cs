using System;
using Unity.VisualScripting;
using UnityEngine;

public class DropsManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private candy candyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
    }

    private void OnDestroy()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPassedAwayCallBack(Vector2 EnemyPosition)
    {
        Instantiate(candyPrefab,EnemyPosition,Quaternion.identity,transform);
    }


}
