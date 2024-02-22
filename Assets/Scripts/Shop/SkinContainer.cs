using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinContainer : MonoBehaviour
{
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI costText;
    private SkinShopAndSelection skinController;
    private PlayerSkinData playerSkinData;
    public PlayerSkinData PlayerSkinData => playerSkinData;
    public void SetShopItem(PlayerSkinData playerSkinData, SkinShopAndSelection controller)
    {
        // Oyuncu kostüm verisini ve kontrolcüyü ayarla
        this.playerSkinData = playerSkinData;
        skinController = controller;

        if (skinController.IsSkinOwned(playerSkinData)) //Bu kostüme sahipse seçilebilir yap
            selectButton.interactable = true;

        // kostüm renk görselinin rengini oyuncu kostüm rengine ayarla
        itemImage.color = playerSkinData.SkinColor;
        costText.text = playerSkinData.Cost.ToString();

        // Satýn alma düðmesinin görünürlüðünü güncelle
        UpdateBuyButtonVisibility();

        // Düðme dinleyicilerini ekle
        AddButtonListeners();
    }

    private void UpdateBuyButtonVisibility()
    {
        // Satýn alma düðmesinin görünürlüðünü, oyuncunun deriyi sahip olup olmadýðýna göre güncelle
        buyButton.gameObject.SetActive(!skinController.IsSkinOwned(playerSkinData));
    }

    private void AddButtonListeners()
    {
        // Satýn alma ve seçme düðmelerine týklama dinleyicilerini ekle
        buyButton.onClick.AddListener(() => OnBuyButtonClicked());
        selectButton.onClick.AddListener(() => OnSelectButtonClicked());
    }
    public void OnBuyButtonClicked()
    {
        // Oyuncu kostümünü satýn al ve dükkân öðesini güncelle
        skinController.PurchasePlayerSkin(playerSkinData);
        SetShopItem(playerSkinData, skinController);
    }    
    public void OnSelectButtonClicked()
    {
        // Oyuncu kostümünü seç
        skinController.SelectPlayerSkin(playerSkinData);
    }
    public void SetSelectionVisual(bool isSelected)
    {
        // Seçim düðmesinin rengini, seçili olup olmadýðýna göre güncelle
        selectButton.GetComponent<Image>().color = isSelected ? selectedColor : normalColor;
    }
}