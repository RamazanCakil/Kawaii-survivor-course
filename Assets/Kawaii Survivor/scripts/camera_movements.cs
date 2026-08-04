using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera_movements : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector2 minMaxXY;


    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned for camera_movements script. '' come ooonnnn ''");
        }
        // Karakterin pozisyonunu alıyoruz
        Vector3 targetPosition = target.position;

        // Kameranın karakteri görebilmesi için Z ekseninde geriye alıyoruz
        targetPosition.z = -10;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);



        // HATA BURADAYDI: target.position yerine, Z değerini ayarladığımız 'targetPosition' değişkenini kullanmalıyız.
        transform.position = targetPosition;
    }
}