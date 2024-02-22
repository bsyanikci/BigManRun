using System.Collections.Generic;
using UnityEngine;
public class GameData
{
    public int currency;
    public int level;
    public int thicknessLevel;
    public int heightLevel;

    public int selectedSkinID;

    public List<PlayerSkinData> ownedSkins;

    public GameData()
    {
        this.currency= 0;
        this.level = 1;
        this.thicknessLevel = 1;
        this.heightLevel = 1;
        ownedSkins = new List<PlayerSkinData>();
    }
}
