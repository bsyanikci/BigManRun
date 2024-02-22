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
        winPanel.transform.localScale = Vector3.zero; //win paneli baþlangýçda gözükmemesi için boyutunu sýfýrla
        lostPanel.transform.localScale = Vector3.zero; //lost paneli baþlangýçda gözükmemesi için boyutunu sýfýrla
        getButton.onClick.AddListener(() => LevelManager.Instance.NextLevel()); //Buttona týklandýðýnda sonraki levele geçmesi için event ekle
        lostPanel.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.RestartLevel()); //Buttona týklandýðýnda leveli yeniden baþlatmasý için event ekle
    }
    public void OpenButton(GameObject gameObject)
    {
        gameObject.SetActive(true); //nesneyi aktifleþtir
    }
    public void CloseButton(GameObject gameObject)
    {
        gameObject.SetActive(false); //nesneyi deaktif et
    }
    public void SetGameStartUI()
    {
        startingPanel.SetActive(false); //start paneli deaktif edip
        hudPanel.SetActive(true); // hud paneli aç 
    }
    public void ShowWinUI()
    {
        rewardText.text = CurrencyManager.Instance.LevelCurrency.ToString(); //ödül textini güncelle
        winPanel.SetActive(true); //win paneli aç
        winPanel.transform.DOScale(1, 1); // do tween paketinin fonksiyonu ile boyutu sýfýrdan 1 e 1 saniye de yükselt
    }
    public void ShowLostUI()
    {
        lostPanel.SetActive(true);  //lost paneli kapat
        lostPanel.transform.DOScale(1, 1); // do tween paketinin fonksiyonu ile boyutu sýfýrdan 1 e 1 saniye de yükselt
    }
    public void UpdateCurrencyText(int currency)
    {
        gameCurrencyText.text = currency.ToString();    //oyun içi para kazanýldýkça texti güncellemek için metod
    }
    public void SetMainCurrencyText()
    {
        totalCurrencyText.text = CurrencyManager.Instance.MainCurrency.ToString(); //oyun içi toplam parayý gösteren texti ayarla
    }
}