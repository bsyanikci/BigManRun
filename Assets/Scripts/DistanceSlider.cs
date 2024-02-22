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
        // Slider bile�enini al
        distanceSlider = GetComponent<Slider>();

        // FinishLine nesnesini bul ve transformunu al
        finishLine = GameObject.Find("FinishLine").transform;

        Initialize();
    }
    private void Initialize()
    {
        // Oyun verilerinden seviyeyi al ve seviye metnini g�ncelle
        int level = DataController.Instance.GameData.level;
        levelText.text = level.ToString();

        // Slider'�n maksimum ve minimum de�erlerini ayarla
        distanceSlider.maxValue = finishLine.position.z - PlayerController.Instance.transform.position.z;
        distanceSlider.minValue = 0;
    }
    private void Update()
    {
        // E�er slider nesnesi etkinse ve PlayerController nesnesi null de�ilse
        if (distanceSlider.gameObject.activeInHierarchy && PlayerController.Instance != null)
        {
            // Toplam mesafeyi hesapla
            float totalDistance = finishLine.position.z - PlayerController.Instance.transform.position.z;

            // Kat edilen mesafeyi hesapla
            float travelledDistance = distanceSlider.maxValue - totalDistance;

            // Slider'�n de�erini kat edilen mesafeye g�re ayarla
            distanceSlider.value = travelledDistance;
        }
    }
}