using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int levelCurrency = 0;
    private int mainCurrency;
    public int LevelCurrency => levelCurrency;
    public int MainCurrency => mainCurrency;
    protected override void Awake()
    {
        base.Awake();
        mainCurrency = DataController.Instance.GameData.currency;   //Baþlangýçda mainCurrency deðiþkenine kaydedilen veriyi ata
    }
    public void AddLevelCurrency(int amount)
    {
        levelCurrency += amount;    //levelCurrenyi amount kadar arttýr
        UIManager.Instance.UpdateCurrencyText(levelCurrency); //ve textini güncelle
    }
    public void AddMainCurrency(int amount) 
    {
        DataController.Instance.GameData.currency += amount;    //GameData'daki currencyi amount kadar arttýr
        DataController.Instance.Save(); //ve kaydet
    }

    public void DecreaseMainCurrency(int amount)
    {
        mainCurrency -= amount;     //anaparayý tutan deðiþkeni miktar kadar azalt
        DataController.Instance.GameData.currency -= amount; //kaydedilen veridende azalt ve kaydet
        DataController.Instance.Save();
    }
    public void MultiplyLevelCurrency(float multiplierValue)
    {
        levelCurrency = Mathf.FloorToInt(levelCurrency * multiplierValue); //levelCurrenyi katla ve düþük integera yuvarla
    }
    public void TransferToMainCurrency()
    {
        AddMainCurrency(levelCurrency); //levelCurrenyi mainCurrenye ekle ve kaydet
        levelCurrency = 0;
    }
}