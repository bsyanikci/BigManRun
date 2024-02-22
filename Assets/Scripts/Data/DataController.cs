using System.IO;
using UnityEngine;

public class DataController : Singleton<DataController>
{
    [SerializeField] private PlayerSkinDatabase skinDB;
    private const string fileName = "Data.txt";     //Data'n�n kaydedilece�i dosya ad�
    private GameData gameData;
    public GameData GameData => gameData;

    protected override void Awake()
    {
        base.Awake();

        Load();
    }
    public void NewGame()
    {
        gameData = new GameData(); //Yeni bir oyun datas� olu�tur
        var defaultSkin = skinDB.GetDefaultPlayerSkin(); // varsay�lan kost�m� bul
        gameData.ownedSkins.Add(defaultSkin); //sahip olunan kost�mlere ekle
        gameData.selectedSkinID = defaultSkin.SkinID; // se�ilen kost�m ID'sini se�ileninID'sini tutan de�i�kene ata
        Save();
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(gameData); //gameData nesnesinin i�indeki verileri json format�na d�n��t�r
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json); //verileri dosyaya yaz
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName); // Cihaz�n kal�c� veri yolu ve dosya ad�n� birle�tirerek dosya yolunu olu�tur
        if (File.Exists(path)) // E�er dosya mevcutsa devam et
        {
            string json = File.ReadAllText(path);         // Dosyadaki JSON verilerini oku
            gameData = JsonUtility.FromJson<GameData>(json); // JSON verilerini GameData t�r�ne d�n��t�r ve gameData de�i�kenine at
        }
        else
        {
            NewGame(); // Dosya mevcut de�ilse yeni bir oyun ba�lat
            Debug.Log("new game");
        }
    }
}
