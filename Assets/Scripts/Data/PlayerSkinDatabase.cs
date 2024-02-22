using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkinData",menuName ="Data/CharacterSkinDB")]
public class PlayerSkinDatabase : ScriptableObject
{
    public List<PlayerSkinData> PlayerSkins = new List<PlayerSkinData>(); //Tüm player kostümlerini bulunduran liste

    public PlayerSkinData GetDefaultPlayerSkin() //Varsayýlan kostümü döndüren bir metod
    {
        PlayerSkinData defaultSkin = PlayerSkins.Find(skin => skin.Cost == 0); //Player kostümleri içerisinde ücreti 0 olaný varsayýlan kostüm olarak ata
        return defaultSkin; //ve bunu döndür
    }
}

[System.Serializable]
public struct PlayerSkinData //Kostüm verilerini tutmak için bir structre
{
    public int SkinID;
    public Color SkinColor;
    public int Cost;
}