
/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Sınıf adı dosya adıyla BİREBİR aynı olmalı ve MonoBehaviour'dan miras almalı
public class Mobilejoystick : MonoBehaviour
{
    [UnitHeaderInspectable("Elements")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;


    [UnitHeaderInspectable("Settings")]
    [SerializeField] private float moveFactor;
    private Vector3 move;
    private Vector3 clickedPosition;
    private bool canControl;

    void Start()
    {
        HideJoystick();
    }

    void Update()
    {
        if (canControl)
            Controljoyistick();


    }
    public void ClickedOnJoystickZoneCallback()
    {
        Vector3 clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;
        ShowJoystick();
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;
    }
    private void Controljoyistick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;

        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        move = direction.normalized * moveMagnitude;

        Vector3 targetPosition = clickedPosition + move;

        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
            HideJoystick();


        
    }


}

*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mobilejoystick : MonoBehaviour
{
    [UnitHeaderInspectable("Elements")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [UnitHeaderInspectable("Settings")]
    [SerializeField] private float moveFactor;

    private Vector3 move;
    private Vector3 clickedPosition;
    private bool canControl;

    void Start()
    {
        HideJoystick();
    }

    void Update()
    {
        if (canControl)
            Controljoyistick();
    }

    public void ClickedOnJoystickZoneCallback()
    {
        // HATA BURADAYDI: Baştaki 'Vector3' kelimesini kaldırdık ki global değişkeni güncellesin.
        clickedPosition = Input.mousePosition;

        // Hem çerçeveyi hem de içindeki topuzu tıkladığımız yere taşıyoruz
        joystickOutline.position = clickedPosition;
        joystickKnob.position = clickedPosition;

        ShowJoystick();
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;
    }

    private void Controljoyistick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;

        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        move = direction.normalized * moveMagnitude;

        Vector3 targetPosition = clickedPosition + move;

        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
            HideJoystick();
    }
}