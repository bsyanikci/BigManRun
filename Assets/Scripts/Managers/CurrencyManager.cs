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
        mainCurrency = DataController.Instance.GameData.currency;   //Ba�lang��da mainCurrency de�i�kenine kaydedilen veriyi ata
    }
    public void AddLevelCurrency(int amount)
    {
        levelCurrency += amount;    //levelCurrenyi amount kadar artt�r
        UIManager.Instance.UpdateCurrencyText(levelCurrency); //ve textini g�ncelle
    }
    public void AddMainCurrency(int amount) 
    {
        DataController.Instance.GameData.currency += amount;    //GameData'daki currencyi amount kadar artt�r
        DataController.Instance.Save(); //ve kaydet
    }

    public void DecreaseMainCurrency(int amount)
    {
        mainCurrency -= amount;     //anaparay� tutan de�i�keni miktar kadar azalt
        DataController.Instance.GameData.currency -= amount; //kaydedilen veridende azalt ve kaydet
        DataController.Instance.Save();
    }
    public void MultiplyLevelCurrency(float multiplierValue)
    {
        levelCurrency = Mathf.FloorToInt(levelCurrency * multiplierValue); //levelCurrenyi katla ve d���k integera yuvarla
    }
    public void TransferToMainCurrency()
    {
        AddMainCurrency(levelCurrency); //levelCurrenyi mainCurrenye ekle ve kaydet
        levelCurrency = 0;
    }
}