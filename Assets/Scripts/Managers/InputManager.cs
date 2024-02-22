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
        EnhancedTouchSupport.Enable(); //Script aktifle�tirildi�inde dokunma destei�ini aktifle�tir
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable(); //Script deaktif edildi�inde dokunma deste�ini deaktif et
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
        inputPosition = Mouse.current.position.ReadValue(); //Farenin �uanki pozisyonunun de�erini oku

        if (Mouse.current.leftButton.isPressed) //Sol click bas�ld�ysa
        {
            if (!hasInput) //Input yoksa
            {
                previousInputPosition = inputPosition; //�nceki input pozisyonuna �uanki fare pozisyonunu ata
            }
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
        #else
        if(Touch.activeTouches.Count > 0) //Aktif dokunma say�s� 0dan b�y�kse
        {
            inputPosition = Touch.activeTouches[0].screenPosition; //aktif dokunmalar�n s�f�r�nc�s�n�n ekrandaki pozisyonunu al

            if (!hasInput)
            {
                previousInputPosition = inputPosition; //�nceki input pozisyonuna �uanki input pozisyonunu ata
            }
            hasInput= true;
        }
        #endif
        if (hasInput) //input varsa
        {
            float normalizedDeltaPosition = (inputPosition.x - previousInputPosition.x) / Screen.width * inputSensivity; //�uanki input pozisyonuyla �nceki input pozisyonu aras�ndaki fark� al ve ekran de�i�li�i ile input hassasiyetini �arp�m�na b�l�p normalle�tir
            PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition); //playerin pozisyonunu normalizedDeltaPozisyon olarak ayarla
        }
        else
        {
            PlayerController.Instance.CancelMovement(); //input yoksa playerin hareketini iptal et
        }
        previousInputPosition = inputPosition;
    }
}