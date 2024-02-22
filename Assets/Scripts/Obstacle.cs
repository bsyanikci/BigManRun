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
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Çarpýþan objenin etiketinin "Player" tagýna sahipse
        {
            HandlePlayerCollision(collision.gameObject); // Oyuncu ile çarpýþma iþlemlerini gerçekleþtir
            SetColliderAsTrigger(); // Collider'ýn isTrigger özelliði true olarak ayarla
            ApplyRandomForce(); //Rastgele kuvvet uygula
            AudioManager.Instance.PlayEffect(sound); //Çarpma sesi çal
        }
    }

    protected virtual void HandlePlayerCollision(GameObject player)
    {
        TransformChanger transformChanger = player.GetComponent<TransformChanger>(); // Oyuncunun TransformChanger bileþenini al
        Transform playerTorso = player.transform.GetChild(0).GetChild(1).transform; // Oyuncunun torsosunu al

        float torsoHeight = playerTorso.localScale.y;  // Torsu yüksekliðini al

        if (torsoHeight > heightChangeThreshold)  // Torsunun yüksekliði belirli bir eþik deðerinden büyükse
        {
            transformChanger.ChangeHeight(heightChangeAmount); // TransformChanger ile oyuncunun yüksekliðini deðiþtir
        }
        else
        {
            transformChanger.ChangeThickness(thicknessChangeAmount); // TransformChanger ile oyuncunun kalýnlýðýný deðiþtir
        }
    }

    protected void SetColliderAsTrigger()
    {
        GetComponent<Collider>().isTrigger = true; // Collider'ýn isTrigger özelliði true olarak ayarla
    }

    protected void ApplyRandomForce()
    {
        // Rastgele kuvvet deðerleri oluþtur
        float forceX = Random.Range(minForceX, maxForceX);
        float forceY = Random.Range(minForceY, maxForceY);
        float forceZ = Random.Range(minForceZ, maxForceZ);

        // Rigidbodoya rastgele bir darbe uygula (Impulse ile)
        rb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
    }
}
