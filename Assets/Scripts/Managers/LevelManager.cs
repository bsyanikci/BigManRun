using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private PlayerController player;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadSceneAsync("LevelScene");
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        int level = DataController.Instance.GameData.level;
        if(levelPrefabs.Count >= level)
        {
            Instantiate(levelPrefabs[level - 1]); 
        }
        else
        {
            Instantiate(levelPrefabs[levelPrefabs.Count - 1]); 
        }
    }
    public void NextLevel()
    {
        DataController.Instance.GameData.level++; //Leveli arttýr
        DataController.Instance.Save();
        LoadSceneAsync("LevelScene");
    }
    public void RestartLevel()
    {
        LoadSceneAsync("LevelScene");
    }
}
