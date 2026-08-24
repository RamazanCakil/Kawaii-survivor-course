using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class candy : DroppableCurrency
{

    [Header("Actions")]
    public static Action<candy> onCollected; 

    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }


}
