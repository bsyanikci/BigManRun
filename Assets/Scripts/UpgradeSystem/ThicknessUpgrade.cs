using UnityEngine;

public class ThicknessUpgrade : IUpgrade
{
    private TransformChanger transformChanger;
    private int upgradeLevel;
    private int defaultUpgradeCost = 50;
    private int maxUpgradeLevel = 50;
    private int costMultiplier = 10;
    private float thicknessValue = 2;
    public int UpgradeLevel => upgradeLevel;
    public ThicknessUpgrade(TransformChanger transformChanger, int upgradeLevel)
    {
        this.transformChanger = transformChanger;
        this.upgradeLevel = upgradeLevel;
    }

    public int CalculateUpgradeCost()
    {
        //Geliþtirme ücretini hesapla
        return defaultUpgradeCost + (upgradeLevel * costMultiplier);
    }

    public bool IsUpgradeable(int upgradeCost)
    {
        // Yükseltme maliyeti, ana para biriminden küçük ve yükseltme seviyesi maksimum yükseltme seviyesinden küçükse true döndür
        return upgradeCost < CurrencyManager.Instance.MainCurrency && upgradeLevel < maxUpgradeLevel;
    }

    public bool Upgrade()
    {
        // Yükseltme maliyetini hesapla
        int upgradeCost = CalculateUpgradeCost();

        // Eðer yükseltme mümkünse
        if (IsUpgradeable(upgradeCost))
        {
            //Transform deðiþtiricide kalýnlýðý yükselt
            transformChanger.ChangeThickness(thicknessValue * transformChanger.DefaultThicknessMultiplier);

            // Yükseltme seviyesini bir arttýr
            upgradeLevel += 1;

            // Ana para birimini azalt
            CurrencyManager.Instance.DecreaseMainCurrency(upgradeCost);

            //Ana para biriminin textini güncelle
            UIManager.Instance.SetMainCurrencyText();

            // Oyun verilerinde kalýnlýk seviyesini bir arttýr ve verileri kaydet
            DataController.Instance.GameData.thicknessLevel += 1;
            DataController.Instance.Save();
            return true;
        }
        return false;
    }
}