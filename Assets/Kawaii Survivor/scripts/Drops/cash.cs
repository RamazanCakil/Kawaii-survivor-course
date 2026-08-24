using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class cash : DroppableCurrency
{

    [Header("Actions")]
    public static Action<cash> onCollected;

    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }


}
