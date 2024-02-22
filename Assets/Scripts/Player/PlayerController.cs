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
        canMove = true; // Hareket etme yetene�ini ba�lang��ta etkinle�tir

        // Ba�lang��ta x ve z pozisyonlar�n� kaydet
        xPos = transform.position.x;
        zPos = transform.position.z;

        // H�z� s�f�rla
        ResetSpeed();
    }

    private void Update()
    {
        // E�er hareket etme yetene�i devre d��� b�rak�lm��sa, i�lemi sonland�r
        if (!canMove)
            return;

        // E�er oyuncu d��erse, �l�m i�lemini ba�lat
        if (transform.position.y < -2f)
        {
            GetComponent<PlayerDead>().Death();
        }

        // Zamanla ilgili bir de�i�keni sakla
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

        // Animator varsa ve deltaTime pozitifse, animasyon h�z�n� g�ncelle
        if (animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (transform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        // E�er pozisyon de�i�tiyse, karakterin y�n�n� g�ncelle
        if (transform.position != lastPosition)
        {
            transform.forward = Vector3.Lerp(transform.forward, (transform.position - lastPosition).normalized, speed);
        }
        lastPosition = transform.position;
    }

    public void AdjustMoveSpeed(float speed)
    {
        // Hedef hareket h�z�n� artt�r veya azalt
        targetMoveSpeed += speed;

        // Hedef hareket h�z�n� en az 0 olarak s�n�rla
        targetMoveSpeed = Mathf.Max(0, targetMoveSpeed);
    }
    public void ResetSpeed()
    {
        // Hedef hareket h�z�n� varsay�lan hareket h�z�na s�f�rla
        targetMoveSpeed = defaultMoveSpeed;
    }
    public void SetDeltaPosition(float normalizedDeltaPosition)
    {
        // X ekseninde tam geni�lik hesapla
        float fullWidth = maxXPosition * 2;

        // Hedef pozisyonu, normalle�tirilmi� delta pozisyonu ile g�ncelle
        targetPosition = targetPosition + fullWidth * normalizedDeltaPosition;

        // Hedef pozisyonu, belirli s�n�rlar i�inde tut
        targetPosition = Mathf.Clamp(targetPosition, -maxXPosition, maxXPosition);
        hasInput = true;
    }
    public void Jump(Vector3 jumpEndPoint, float jumpPower, float duration)
    {
        // �u anki y�kseklik pozisyonunu sakla
        float currentYPos = transform.position.y;

        // Z�plama hedefinin y eksenini, �u anki y�kseklik pozisyonu ile e�itle
        jumpEndPoint.y = currentYPos;

        // Hareketi devre d��� b�rak
        canMove = false;

        // DOTween k�t�phanesi ile karakteri z�platarak, tamamland���nda tekrar hareket etmeyi sa�la
        transform.DOJump(jumpEndPoint, jumpPower, 1, duration).OnComplete(() =>
        {
            canMove = true;

            // X ve Z pozisyonlar�n� z�plama noktas�na g�ncelle
            xPos = jumpEndPoint.x;
            zPos = jumpEndPoint.z;
        });
    }
    private void Accelerate(float deltaTime, float targetSpeed)
    {
        // Hareket h�z�n� h�zland�r
        moveSpeed += deltaTime * accelerationSpeed;

        // Hareket h�z�n� hedef h�z ile s�n�rla
        moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
    }
    private void Decelerate(float deltaTime, float targetSpeed)
    {
        // Hareket h�z�n� yava�lat
        moveSpeed -= deltaTime * decelerationSpeed;

        // Hareket h�z�n� hedef h�z ile s�n�rla
        moveSpeed = Mathf.Max(moveSpeed, targetSpeed);
    }
    public void CancelMovement()
    {
        // Hareket giri�ini iptal et
        hasInput = false;
    }
    public void SetAnimator(bool active)
    {
        // Animator'�n etkinli�ini belirtilen duruma ayarla
        animator.enabled = false;
    }
    public void SetMaxXPosition(float value)
    {
        // Maksimum X pozisyonunu belirtilen de�ere ayarla
        maxXPosition = value;
    }
    public Vector3 GetPlayerTop()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        // Oyuncunun tepesini, kaps�l collider'�n merkezi ve y�ksekli�inin yar�s� kadar yukar�dan hesapla
        return transform.position + col.center + Vector3.up * (col.height * 0.5f);
    }
}