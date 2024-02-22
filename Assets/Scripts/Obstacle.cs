using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private SoundID sound = SoundID.None;

    [Header("Configuration")]
    [SerializeField] protected float heightChangeThreshold = 0.5f;
    [SerializeField] protected float heightChangeAmount = -0.15f;
    [SerializeField] protected float thicknessChangeAmount = -0.1f;

    [Header("Random Force")]
    [SerializeField] protected float minForceX = -10f;
    [SerializeField] protected float maxForceX = 10f;
    [SerializeField] protected float minForceY = 8f;
    [SerializeField] protected float maxForceY = 12f;
    [SerializeField] protected float minForceZ = 8f;
    [SerializeField] protected float maxForceZ = 12f;


    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �arp��an objenin etiketinin "Player" tag�na sahipse
        {
            HandlePlayerCollision(collision.gameObject); // Oyuncu ile �arp��ma i�lemlerini ger�ekle�tir
            SetColliderAsTrigger(); // Collider'�n isTrigger �zelli�i true olarak ayarla
            ApplyRandomForce(); //Rastgele kuvvet uygula
            AudioManager.Instance.PlayEffect(sound); //�arpma sesi �al
        }
    }

    protected virtual void HandlePlayerCollision(GameObject player)
    {
        TransformChanger transformChanger = player.GetComponent<TransformChanger>(); // Oyuncunun TransformChanger bile�enini al
        Transform playerTorso = player.transform.GetChild(0).GetChild(1).transform; // Oyuncunun torsosunu al

        float torsoHeight = playerTorso.localScale.y;  // Torsu y�ksekli�ini al

        if (torsoHeight > heightChangeThreshold)  // Torsunun y�ksekli�i belirli bir e�ik de�erinden b�y�kse
        {
            transformChanger.ChangeHeight(heightChangeAmount); // TransformChanger ile oyuncunun y�ksekli�ini de�i�tir
        }
        else
        {
            transformChanger.ChangeThickness(thicknessChangeAmount); // TransformChanger ile oyuncunun kal�nl���n� de�i�tir
        }
    }

    protected void SetColliderAsTrigger()
    {
        GetComponent<Collider>().isTrigger = true; // Collider'�n isTrigger �zelli�i true olarak ayarla
    }

    protected void ApplyRandomForce()
    {
        // Rastgele kuvvet de�erleri olu�tur
        float forceX = Random.Range(minForceX, maxForceX);
        float forceY = Random.Range(minForceY, maxForceY);
        float forceZ = Random.Range(minForceZ, maxForceZ);

        // Rigidbodoya rastgele bir darbe uygula (Impulse ile)
        rb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
    }
}
