using System;
using UnityEngine;

public class chest : MonoBehaviour,Icollectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Actions")]
    public static Action onCollected;

    public void Collect(Player player) {
        onCollected?.Invoke();
        Destroy(gameObject);
    }

}
