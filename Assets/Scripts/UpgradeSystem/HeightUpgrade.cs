using UnityEngine;

public class HeightUpgrade : IUpgrade
{
    private TransformChanger transformChanger;
    private int upgradeLevel;
    private int defaultUpgradeCost = 50;
    private int maxUpgradeLevel = 50;
    private int costMultiplier = 10;
    private float heightValue = 3;
    public int UpgradeLevel => upgradeLevel;
    public HeightUpgrade(TransformChanger transformChanger, int upgradeLevel)
    {
        this.transformChanger = transformChanger;
        this.upgradeLevel = upgradeLevel;
    }

    public int CalculateUpgradeCost()
    {
        //Geli�tirme �cretini hesapla
        return defaultUpgradeCost + (upgradeLevel * costMultiplier);
    }

    public bool IsUpgradeable(int upgradeCost)
    {
        // Y�kseltme maliyeti, ana para biriminden k���k ve y�kseltme seviyesi maksimum y�kseltme seviyesinden k���kse true d�nd�r
        return upgradeCost < CurrencyManager.Instance.MainCurrency && upgradeLevel < maxUpgradeLevel;
    }

    public bool Upgrade()
    {
        // Y�kseltme maliyetini hesapla
        int upgradeCost = CalculateUpgradeCost();

        // E�er y�kseltme m�mk�nse
        if (IsUpgradeable(upgradeCost))
        {
            //Transform de�i�tiricide y�ksekli�i y�kselt
            transformChanger.ChangeHeight(heightValue * transformChanger.DefaultHeightMultiplier);

            // Y�kseltme seviyesini bir artt�r
            upgradeLevel += 1;

            // Ana para birimini azalt
            CurrencyManager.Instance.DecreaseMainCurrency(upgradeCost);

            //Ana para biriminin textini g�ncelle
            UIManager.Instance.SetMainCurrencyText();

            // Oyun verilerinde y�kseklik seviyesini bir artt�r ve verileri kaydet
            DataController.Instance.GameData.heightLevel += 1;
            DataController.Instance.Save();
            return true;
        }
        return false;
    }
}