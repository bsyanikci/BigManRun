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
        // Kost�m veritaban�ndaki her oyuncu kost�m� i�in d�ng� ba�lat
        for (int i = 0; i< skinDB.PlayerSkins.Count; i++)
        {
            // Sat�n alma ��esini klonla ve ba�lam� ayarla
            SkinContainer shopItem = Instantiate(skinContainer, skinContentTransform);
            shopItem.SetShopItem(skinDB.PlayerSkins[i], this);
        }
        // Se�ilen kost�m�n ID'sini al
        selectedSkinID = DataController.Instance.GameData.selectedSkinID;

        // Se�ilen kost�m�n rengini belirle
        Color selectedSkinColor = skinDB.PlayerSkins.Find(skin => skin.SkinID == selectedSkinID).SkinColor;
        
        // Oyuncu kost�m�n�n rengini g�ncelle
        playerSkinMat.color = selectedSkinColor;

        UpdateSkinSelectionVisual();
    }

    private void UpdateSkinSelectionVisual()
    {
        // T�m kost�m ��elerini kontrol et ve se�ilip se�ilmedi�ine g�re g�rselini g�ncelle
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
        // E�er kost�me sahip de�ilse ve sat�n al�nabilirse
        if (!IsSkinOwned(skin) && IsPurchasable(skin.Cost))
        {
            // Ana para birimini azalt
            CurrencyManager.Instance.DecreaseMainCurrency(skin.Cost);

            // UI �zerinde ana para birimini g�ncelle
            UIManager.Instance.SetMainCurrencyText();
            
            // Sat�n al�nan deriyi ekle ve kaydet
            DataController.Instance.GameData.ownedSkins.Add(skin);
            DataController.Instance.Save();
        }
    }
    public void SelectPlayerSkin(PlayerSkinData skin)
    {
        // Se�ilen kost�m�n ID'sini g�ncelle
        selectedSkinID = skin.SkinID;

        // Oyuncu kostum�n�n rengini g�ncelle
        playerSkinMat.color = skin.SkinColor;
        UpdateSkinSelectionVisual();

        // Oyun verilerine se�ilen kost�m� ata ve kaydet
        DataController.Instance.GameData.selectedSkinID = skin.SkinID;
        DataController.Instance.Save();
    }
    public bool IsSkinOwned(PlayerSkinData skin)
    {
        // Kost�m, sahip olunan kost�mler listesinde bulunuyorsa true d�nd�r
        return DataController.Instance.GameData.ownedSkins.Contains(skin);
    }
    public bool IsPurchasable(int cost)
    {
        // Ana para biriminin, maliyetten b�y�k veya e�it olup olmad���n� kontrol et
        return CurrencyManager.Instance.MainCurrency >= cost;
    }
}