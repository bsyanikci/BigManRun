using TMPro;
using UnityEngine;

public class SkinShopAndSelection : MonoBehaviour
{
    [SerializeField] private PlayerSkinDatabase skinDB;
    [SerializeField] private Material playerSkinMat;
    [SerializeField] private SkinContainer skinContainer;
    [SerializeField] private Transform skinContentTransform;
    [SerializeField] private TextMeshProUGUI currencyText;

    private int selectedSkinID;

    private void Awake()
    {
        // Kostüm veritabanýndaki her oyuncu kostümü için döngü baþlat
        for (int i = 0; i< skinDB.PlayerSkins.Count; i++)
        {
            // Satýn alma öðesini klonla ve baðlamý ayarla
            SkinContainer shopItem = Instantiate(skinContainer, skinContentTransform);
            shopItem.SetShopItem(skinDB.PlayerSkins[i], this);
        }
        // Seçilen kostümün ID'sini al
        selectedSkinID = DataController.Instance.GameData.selectedSkinID;

        // Seçilen kostümün rengini belirle
        Color selectedSkinColor = skinDB.PlayerSkins.Find(skin => skin.SkinID == selectedSkinID).SkinColor;
        
        // Oyuncu kostümünün rengini güncelle
        playerSkinMat.color = selectedSkinColor;

        UpdateSkinSelectionVisual();
    }

    private void UpdateSkinSelectionVisual()
    {
        // Tüm kostüm öðelerini kontrol et ve seçilip seçilmediðine göre görselini güncelle
        foreach (Transform child in skinContentTransform)
        {
            SkinContainer skinContainer = child.GetComponent<SkinContainer>();
            if (skinContainer != null)
            {
                PlayerSkinData skinData = skinContainer.PlayerSkinData;
                skinContainer.SetSelectionVisual(selectedSkinID == skinData.SkinID);
            }
        }
    }

    public void PurchasePlayerSkin(PlayerSkinData skin)
    {
        // Eðer kostüme sahip deðilse ve satýn alýnabilirse
        if (!IsSkinOwned(skin) && IsPurchasable(skin.Cost))
        {
            // Ana para birimini azalt
            CurrencyManager.Instance.DecreaseMainCurrency(skin.Cost);

            // UI üzerinde ana para birimini güncelle
            UIManager.Instance.SetMainCurrencyText();
            
            // Satýn alýnan deriyi ekle ve kaydet
            DataController.Instance.GameData.ownedSkins.Add(skin);
            DataController.Instance.Save();
        }
    }
    public void SelectPlayerSkin(PlayerSkinData skin)
    {
        // Seçilen kostümün ID'sini güncelle
        selectedSkinID = skin.SkinID;

        // Oyuncu kostumünün rengini güncelle
        playerSkinMat.color = skin.SkinColor;
        UpdateSkinSelectionVisual();

        // Oyun verilerine seçilen kostümü ata ve kaydet
        DataController.Instance.GameData.selectedSkinID = skin.SkinID;
        DataController.Instance.Save();
    }
    public bool IsSkinOwned(PlayerSkinData skin)
    {
        // Kostüm, sahip olunan kostümler listesinde bulunuyorsa true döndür
        return DataController.Instance.GameData.ownedSkins.Contains(skin);
    }
    public bool IsPurchasable(int cost)
    {
        // Ana para biriminin, maliyetten büyük veya eþit olup olmadýðýný kontrol et
        return CurrencyManager.Instance.MainCurrency >= cost;
    }
}