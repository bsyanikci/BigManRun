using System.IO;
using UnityEngine;

public class DataController : Singleton<DataController>
{
    [SerializeField] private PlayerSkinDatabase skinDB;
    private const string fileName = "Data.txt";     //Data'nýn kaydedileceði dosya adý
    private GameData gameData;
    public GameData GameData => gameData;

    protected override void Awake()
    {
        base.Awake();

        Load();
    }
    public void NewGame()
    {
        gameData = new GameData(); //Yeni bir oyun datasý oluþtur
        var defaultSkin = skinDB.GetDefaultPlayerSkin(); // varsayýlan kostümü bul
        gameData.ownedSkins.Add(defaultSkin); //sahip olunan kostümlere ekle
        gameData.selectedSkinID = defaultSkin.SkinID; // seçilen kostüm ID'sini seçileninID'sini tutan deðiþkene ata
        Save();
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(gameData); //gameData nesnesinin içindeki verileri json formatýna dönüþtür
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json); //verileri dosyaya yaz
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName); // Cihazýn kalýcý veri yolu ve dosya adýný birleþtirerek dosya yolunu oluþtur
        if (File.Exists(path)) // Eðer dosya mevcutsa devam et
        {
            string json = File.ReadAllText(path);         // Dosyadaki JSON verilerini oku
            gameData = JsonUtility.FromJson<GameData>(json); // JSON verilerini GameData türüne dönüþtür ve gameData deðiþkenine at
        }
        else
        {
            NewGame(); // Dosya mevcut deðilse yeni bir oyun baþlat
            Debug.Log("new game");
        }
    }
}
