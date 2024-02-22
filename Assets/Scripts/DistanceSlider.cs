using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private Slider distanceSlider;
    private Transform finishLine;
    private void Awake()
    {
        // Slider bileþenini al
        distanceSlider = GetComponent<Slider>();

        // FinishLine nesnesini bul ve transformunu al
        finishLine = GameObject.Find("FinishLine").transform;

        Initialize();
    }
    private void Initialize()
    {
        // Oyun verilerinden seviyeyi al ve seviye metnini güncelle
        int level = DataController.Instance.GameData.level;
        levelText.text = level.ToString();

        // Slider'ýn maksimum ve minimum deðerlerini ayarla
        distanceSlider.maxValue = finishLine.position.z - PlayerController.Instance.transform.position.z;
        distanceSlider.minValue = 0;
    }
    private void Update()
    {
        // Eðer slider nesnesi etkinse ve PlayerController nesnesi null deðilse
        if (distanceSlider.gameObject.activeInHierarchy && PlayerController.Instance != null)
        {
            // Toplam mesafeyi hesapla
            float totalDistance = finishLine.position.z - PlayerController.Instance.transform.position.z;

            // Kat edilen mesafeyi hesapla
            float travelledDistance = distanceSlider.maxValue - totalDistance;

            // Slider'ýn deðerini kat edilen mesafeye göre ayarla
            distanceSlider.value = travelledDistance;
        }
    }
}