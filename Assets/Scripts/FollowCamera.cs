using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 lookAtOffset;
    [SerializeField] private float SmoothCameraFollowStrength;
    
    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = transform;
    }

    private Vector3 GetPlayerPosition()
    {
        // Oyuncu pozisyonunu varsay�lan olarak yukar� vekt�r�ne ayarla
        Vector3 playerPosition = Vector3.up;
        if(PlayerController.Instance != null)
        {
            // Oyuncunun �st pozisyonunu al
            playerPosition = PlayerController.Instance.GetPlayerTop();
        }
        return playerPosition; // Oyuncu pozisyonunu d�nd�r
    }
    private void LateUpdate()
    {
        if(cameraTransform == null)
        {
            return;
        }

        // Kamera pozisyonunu ve y�nelimini ayarla
        SetCameraPositionAndOrientation();
    }

    private void SetCameraPositionAndOrientation()
    {
        // Oyuncu pozisyonunu al
        Vector3 playerPosition = GetPlayerPosition();

        // Kamera pozisyonunu ve bak�� a��s�n� belirli bir ofset ile ayarla
        Vector3 offset = playerPosition + this.offset;
        Vector3 lookAtOffset = playerPosition + this.lookAtOffset;

        // Lerp miktar�n� belirle (Time.deltaTime ile �arp�larak yumu�ak bir takip sa�lan�r)
        float lerpAmount = Time.deltaTime * SmoothCameraFollowStrength;

        // Kamera pozisyonunu yumu�ak bir ge�i� ile ayarla
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, offset, lerpAmount);

        // Kamera bak�� a��s�n�, belirli bir ofset ile ayarla (Zaman.deltaS�resi ile �arp�larak yumu�ak bir takip sa�lan�r)
        cameraTransform.LookAt(Vector3.Lerp(cameraTransform.position + cameraTransform.forward,lookAtOffset,lerpAmount));

        // Kameran�n yaln�zca z eksenini sabitle (sadece x ve y eksenlerinde takip yap)
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, offset.z);
    }
}
