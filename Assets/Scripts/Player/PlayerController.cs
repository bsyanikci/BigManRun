using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerHead;
    [SerializeField] private float customPlayerSpeed = 10.0f;
    [SerializeField] private float accelerationSpeed = 10.0f;
    [SerializeField] private float decelerationSpeed = 20.0f;
    [SerializeField] private float horizontalSpeedFactor = 0.5f;
    [SerializeField] private float maxXPosition = 5f;
    [SerializeField] private float defaultMoveSpeed = 10;

    private Vector3 lastPosition;
    private bool canMove;
    private bool hasInput;
    private float xPos;
    private float zPos;

    private float moveSpeed;
    private float targetMoveSpeed;
    private float targetPosition;

    public float MoveSpeed => moveSpeed;
    public float TargetMoveSpeed => targetMoveSpeed;
    public float MaxXPoisiton => maxXPosition;
    public float TargetPosition => targetPosition;

    protected override void Awake()
    {
        canMove = true; // Hareket etme yeteneðini baþlangýçta etkinleþtir

        // Baþlangýçta x ve z pozisyonlarýný kaydet
        xPos = transform.position.x;
        zPos = transform.position.z;

        // Hýzý sýfýrla
        ResetSpeed();
    }

    private void Update()
    {
        // Eðer hareket etme yeteneði devre dýþý býrakýlmýþsa, iþlemi sonlandýr
        if (!canMove)
            return;

        // Eðer oyuncu düþerse, ölüm iþlemini baþlat
        if (transform.position.y < -2f)
        {
            GetComponent<PlayerDead>().Death();
        }

        // Zamanla ilgili bir deðiþkeni sakla
        float deltaTime = Time.deltaTime;

        //Update Speed
        if (!hasInput)
        {
            Decelerate(deltaTime, 0.0f);
        }
        else if (targetMoveSpeed < moveSpeed)
        {
            Decelerate(deltaTime, targetMoveSpeed);
        }
        else if (targetMoveSpeed > moveSpeed)
        {
            Accelerate(deltaTime, targetMoveSpeed);
        }
        float speed = moveSpeed * deltaTime;

        //Update Position
        zPos += speed;

        if (hasInput)
        {
            float horizontalSpeed = speed * horizontalSpeedFactor;
            float newPositionTarget = Mathf.Lerp(xPos, targetPosition, horizontalSpeed);
            float newPositionDifference = newPositionTarget - xPos;

            newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
            xPos += newPositionDifference;
        }
        transform.position = new Vector3(xPos, transform.position.y, zPos);

        // Animator varsa ve deltaTime pozitifse, animasyon hýzýný güncelle
        if (animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (transform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        // Eðer pozisyon deðiþtiyse, karakterin yönünü güncelle
        if (transform.position != lastPosition)
        {
            transform.forward = Vector3.Lerp(transform.forward, (transform.position - lastPosition).normalized, speed);
        }
        lastPosition = transform.position;
    }

    public void AdjustMoveSpeed(float speed)
    {
        // Hedef hareket hýzýný arttýr veya azalt
        targetMoveSpeed += speed;

        // Hedef hareket hýzýný en az 0 olarak sýnýrla
        targetMoveSpeed = Mathf.Max(0, targetMoveSpeed);
    }
    public void ResetSpeed()
    {
        // Hedef hareket hýzýný varsayýlan hareket hýzýna sýfýrla
        targetMoveSpeed = defaultMoveSpeed;
    }
    public void SetDeltaPosition(float normalizedDeltaPosition)
    {
        // X ekseninde tam geniþlik hesapla
        float fullWidth = maxXPosition * 2;

        // Hedef pozisyonu, normalleþtirilmiþ delta pozisyonu ile güncelle
        targetPosition = targetPosition + fullWidth * normalizedDeltaPosition;

        // Hedef pozisyonu, belirli sýnýrlar içinde tut
        targetPosition = Mathf.Clamp(targetPosition, -maxXPosition, maxXPosition);
        hasInput = true;
    }
    public void Jump(Vector3 jumpEndPoint, float jumpPower, float duration)
    {
        // Þu anki yükseklik pozisyonunu sakla
        float currentYPos = transform.position.y;

        // Zýplama hedefinin y eksenini, þu anki yükseklik pozisyonu ile eþitle
        jumpEndPoint.y = currentYPos;

        // Hareketi devre dýþý býrak
        canMove = false;

        // DOTween kütüphanesi ile karakteri zýplatarak, tamamlandýðýnda tekrar hareket etmeyi saðla
        transform.DOJump(jumpEndPoint, jumpPower, 1, duration).OnComplete(() =>
        {
            canMove = true;

            // X ve Z pozisyonlarýný zýplama noktasýna güncelle
            xPos = jumpEndPoint.x;
            zPos = jumpEndPoint.z;
        });
    }
    private void Accelerate(float deltaTime, float targetSpeed)
    {
        // Hareket hýzýný hýzlandýr
        moveSpeed += deltaTime * accelerationSpeed;

        // Hareket hýzýný hedef hýz ile sýnýrla
        moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
    }
    private void Decelerate(float deltaTime, float targetSpeed)
    {
        // Hareket hýzýný yavaþlat
        moveSpeed -= deltaTime * decelerationSpeed;

        // Hareket hýzýný hedef hýz ile sýnýrla
        moveSpeed = Mathf.Max(moveSpeed, targetSpeed);
    }
    public void CancelMovement()
    {
        // Hareket giriþini iptal et
        hasInput = false;
    }
    public void SetAnimator(bool active)
    {
        // Animator'ýn etkinliðini belirtilen duruma ayarla
        animator.enabled = false;
    }
    public void SetMaxXPosition(float value)
    {
        // Maksimum X pozisyonunu belirtilen deðere ayarla
        maxXPosition = value;
    }
    public Vector3 GetPlayerTop()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        // Oyuncunun tepesini, kapsül collider'ýn merkezi ve yüksekliðinin yarýsý kadar yukarýdan hesapla
        return transform.position + col.center + Vector3.up * (col.height * 0.5f);
    }
}