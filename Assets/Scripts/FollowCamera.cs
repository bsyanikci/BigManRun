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
        // Oyuncu pozisyonunu varsayýlan olarak yukarý vektörüne ayarla
        Vector3 playerPosition = Vector3.up;
        if(PlayerController.Instance != null)
        {
            // Oyuncunun üst pozisyonunu al
            playerPosition = PlayerController.Instance.GetPlayerTop();
        }
        return playerPosition; // Oyuncu pozisyonunu döndür
    }
    private void LateUpdate()
    {
        if(cameraTransform == null)
        {
            return;
        }

        // Kamera pozisyonunu ve yönelimini ayarla
        SetCameraPositionAndOrientation();
    }

    private void SetCameraPositionAndOrientation()
    {
        // Oyuncu pozisyonunu al
        Vector3 playerPosition = GetPlayerPosition();

        // Kamera pozisyonunu ve bakýþ açýsýný belirli bir ofset ile ayarla
        Vector3 offset = playerPosition + this.offset;
        Vector3 lookAtOffset = playerPosition + this.lookAtOffset;

        // Lerp miktarýný belirle (Time.deltaTime ile çarpýlarak yumuþak bir takip saðlanýr)
        float lerpAmount = Time.deltaTime * SmoothCameraFollowStrength;

        // Kamera pozisyonunu yumuþak bir geçiþ ile ayarla
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, offset, lerpAmount);

        // Kamera bakýþ açýsýný, belirli bir ofset ile ayarla (Zaman.deltaSüresi ile çarpýlarak yumuþak bir takip saðlanýr)
        cameraTransform.LookAt(Vector3.Lerp(cameraTransform.position + cameraTransform.forward,lookAtOffset,lerpAmount));

        // Kameranýn yalnýzca z eksenini sabitle (sadece x ve y eksenlerinde takip yap)
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, offset.z);
    }
}
