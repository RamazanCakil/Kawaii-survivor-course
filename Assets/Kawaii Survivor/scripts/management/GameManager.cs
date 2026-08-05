using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{


    // Start, script aktif olduğunda ilk karede (frame) bir kez çalışır
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update, oyun çalıştığı sürece her karede (frame) sürekli olarak çalışır
    void Update()
    {
        
    }
}