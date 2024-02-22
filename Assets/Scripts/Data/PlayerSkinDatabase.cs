using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkinData",menuName ="Data/CharacterSkinDB")]
public class PlayerSkinDatabase : ScriptableObject
{
    public List<PlayerSkinData> PlayerSkins = new List<PlayerSkinData>(); //T�m player kost�mlerini bulunduran liste

    public PlayerSkinData GetDefaultPlayerSkin() //Varsay�lan kost�m� d�nd�ren bir metod
    {
        PlayerSkinData defaultSkin = PlayerSkins.Find(skin => skin.Cost == 0); //Player kost�mleri i�erisinde �creti 0 olan� varsay�lan kost�m olarak ata
        return defaultSkin; //ve bunu d�nd�r
    }
}

[System.Serializable]
public struct PlayerSkinData //Kost�m verilerini tutmak i�in bir structre
{
    public int SkinID;
    public Color SkinColor;
    public int Cost;
}