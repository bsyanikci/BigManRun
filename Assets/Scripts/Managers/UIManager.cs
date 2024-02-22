using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TextMeshProUGUI gameCurrencyText;
    [SerializeField] private GameObject lostPanel;

    [Header("Starting Panel Elements")]
    [SerializeField] private GameObject startingPanel;
    [SerializeField] private TextMeshProUGUI totalCurrencyText;
    
    [Header("Win Panel Elements")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button getButton;

    protected override void Awake()
    {
        base.Awake();
        SetMainCurrencyText();
        winPanel.transform.localScale = Vector3.zero; //win paneli ba�lang��da g�z�kmemesi i�in boyutunu s�f�rla
        lostPanel.transform.localScale = Vector3.zero; //lost paneli ba�lang��da g�z�kmemesi i�in boyutunu s�f�rla
        getButton.onClick.AddListener(() => LevelManager.Instance.NextLevel()); //Buttona t�kland���nda sonraki levele ge�mesi i�in event ekle
        lostPanel.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.RestartLevel()); //Buttona t�kland���nda leveli yeniden ba�latmas� i�in event ekle
    }
    public void OpenButton(GameObject gameObject)
    {
        gameObject.SetActive(true); //nesneyi aktifle�tir
    }
    public void CloseButton(GameObject gameObject)
    {
        gameObject.SetActive(false); //nesneyi deaktif et
    }
    public void SetGameStartUI()
    {
        startingPanel.SetActive(false); //start paneli deaktif edip
        hudPanel.SetActive(true); // hud paneli a� 
    }
    public void ShowWinUI()
    {
        rewardText.text = CurrencyManager.Instance.LevelCurrency.ToString(); //�d�l textini g�ncelle
        winPanel.SetActive(true); //win paneli a�
        winPanel.transform.DOScale(1, 1); // do tween paketinin fonksiyonu ile boyutu s�f�rdan 1 e 1 saniye de y�kselt
    }
    public void ShowLostUI()
    {
        lostPanel.SetActive(true);  //lost paneli kapat
        lostPanel.transform.DOScale(1, 1); // do tween paketinin fonksiyonu ile boyutu s�f�rdan 1 e 1 saniye de y�kselt
    }
    public void UpdateCurrencyText(int currency)
    {
        gameCurrencyText.text = currency.ToString();    //oyun i�i para kazan�ld�k�a texti g�ncellemek i�in metod
    }
    public void SetMainCurrencyText()
    {
        totalCurrencyText.text = CurrencyManager.Instance.MainCurrency.ToString(); //oyun i�i toplam paray� g�steren texti ayarla
    }
}