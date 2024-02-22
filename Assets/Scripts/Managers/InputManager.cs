using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class InputManager : Singleton<InputManager>
{
    private Vector3 inputPosition;
    private Vector3 previousInputPosition;
    private bool hasInput;

    [SerializeField]
    private float inputSensivity;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable(); //Script aktifleþtirildiðinde dokunma desteiðini aktifleþtir
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable(); //Script deaktif edildiðinde dokunma desteðini deaktif et
    }
    private void Update()
    {
        if (PlayerController.Instance == null)
        {
            return;
        }

        if (!GameManager.Instance.IsGameStarted)
        {
            return;
        }

        #if UNITY_EDITOR
        inputPosition = Mouse.current.position.ReadValue(); //Farenin þuanki pozisyonunun deðerini oku

        if (Mouse.current.leftButton.isPressed) //Sol click basýldýysa
        {
            if (!hasInput) //Input yoksa
            {
                previousInputPosition = inputPosition; //önceki input pozisyonuna þuanki fare pozisyonunu ata
            }
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
        #else
        if(Touch.activeTouches.Count > 0) //Aktif dokunma sayýsý 0dan büyükse
        {
            inputPosition = Touch.activeTouches[0].screenPosition; //aktif dokunmalarýn sýfýrýncýsýnýn ekrandaki pozisyonunu al

            if (!hasInput)
            {
                previousInputPosition = inputPosition; //önceki input pozisyonuna þuanki input pozisyonunu ata
            }
            hasInput= true;
        }
        #endif
        if (hasInput) //input varsa
        {
            float normalizedDeltaPosition = (inputPosition.x - previousInputPosition.x) / Screen.width * inputSensivity; //þuanki input pozisyonuyla önceki input pozisyonu arasýndaki farký al ve ekran deðiþliði ile input hassasiyetini çarpýmýna bölüp normalleþtir
            PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition); //playerin pozisyonunu normalizedDeltaPozisyon olarak ayarla
        }
        else
        {
            PlayerController.Instance.CancelMovement(); //input yoksa playerin hareketini iptal et
        }
        previousInputPosition = inputPosition;
    }
}