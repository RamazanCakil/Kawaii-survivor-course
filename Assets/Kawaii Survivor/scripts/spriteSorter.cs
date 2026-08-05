using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class renderingobject : MonoBehaviour
{

    [UnitHeaderInspectable ("Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    

    // Start, script aktif olduğunda ilk karede (frame) bir kez çalışır
    void Start()
    {

    }

    // Update, oyun çalıştığı sürece her karede (frame) sürekli olarak çalışır
    void Update()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 10);



    }


  
}