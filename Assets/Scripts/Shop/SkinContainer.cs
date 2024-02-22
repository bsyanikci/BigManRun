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
        // Oyuncu kost�m verisini ve kontrolc�y� ayarla
        this.playerSkinData = playerSkinData;
        skinController = controller;

        if (skinController.IsSkinOwned(playerSkinData)) //Bu kost�me sahipse se�ilebilir yap
            selectButton.interactable = true;

        // kost�m renk g�rselinin rengini oyuncu kost�m rengine ayarla
        itemImage.color = playerSkinData.SkinColor;
        costText.text = playerSkinData.Cost.ToString();

        // Sat�n alma d��mesinin g�r�n�rl���n� g�ncelle
        UpdateBuyButtonVisibility();

        // D��me dinleyicilerini ekle
        AddButtonListeners();
    }

    private void UpdateBuyButtonVisibility()
    {
        // Sat�n alma d��mesinin g�r�n�rl���n�, oyuncunun deriyi sahip olup olmad���na g�re g�ncelle
        buyButton.gameObject.SetActive(!skinController.IsSkinOwned(playerSkinData));
    }

    private void AddButtonListeners()
    {
        // Sat�n alma ve se�me d��melerine t�klama dinleyicilerini ekle
        buyButton.onClick.AddListener(() => OnBuyButtonClicked());
        selectButton.onClick.AddListener(() => OnSelectButtonClicked());
    }
    public void OnBuyButtonClicked()
    {
        // Oyuncu kost�m�n� sat�n al ve d�kk�n ��esini g�ncelle
        skinController.PurchasePlayerSkin(playerSkinData);
        SetShopItem(playerSkinData, skinController);
    }    
    public void OnSelectButtonClicked()
    {
        // Oyuncu kost�m�n� se�
        skinController.SelectPlayerSkin(playerSkinData);
    }
    public void SetSelectionVisual(bool isSelected)
    {
        // Se�im d��mesinin rengini, se�ili olup olmad���na g�re g�ncelle
        selectButton.GetComponent<Image>().color = isSelected ? selectedColor : normalColor;
    }
}