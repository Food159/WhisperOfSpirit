using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LevelType
{
    Menu, Game
}
public class SceneDataHandler : MonoBehaviour
{
    public string scene;
    public LevelType level;
    public BossPhase phase;
    public static SceneDataHandler instance;
    [SerializeField] UnityEngine.UI.Button buttonContinue;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(level != LevelType.Menu)
        {
            SceneData scenedata = LoadSceneData();
            if (scenedata != null)
            {
                if (level != LevelType.Menu)
                {
                    scene = SceneManager.GetActiveScene().name;
                    SaveSceneData();
                }
            }
            else
            {
                if (level != LevelType.Menu)
                {
                    scene = SceneManager.GetActiveScene().name;
                    SaveSceneData();
                }
                else if (level == LevelType.Menu)
                {
                    buttonContinue.enabled = false;
                    Debug.Log("NoScene");
                }
            }
        } 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveSceneData();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearSceneData();
        }
    }
    public void SaveSceneData()
    {
        if (phase == BossPhase.phase2)
            return;
        scene = SceneManager.GetActiveScene().name;

        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        SceneData sceneData = new SceneData();

        sceneData.sceneName = scene;

        string gameDataJson = JsonUtility.ToJson(sceneData);
        File.WriteAllText(Application.dataPath + "/gameSceneData.json", gameDataJson);
        Debug.Log("Save gameScene data");
    }
    public SceneData LoadSceneData()
    {
        if (File.Exists(Application.dataPath + "/gameSceneData.json") == false)
        {
            return null;
        }
        string loadedGameDataToJson = File.ReadAllText(Application.dataPath + "/gameSceneData.json");
        SceneData loadedGameData = JsonUtility.FromJson<SceneData>(loadedGameDataToJson);
        Debug.Log("LoadSceneData");
        return loadedGameData;
    }
    public void ClearSceneData()
    {
        string pathScene = Application.dataPath + "/gameSceneData.json";
        if (File.Exists(pathScene))
        {
            File.Delete(pathScene);
            Debug.Log("deleted saveScene");
        }
        else
        {
            Debug.Log("No scene data to delete");
        }
    }
    //public void OnApplicationQuit()
    //{
    //    SaveData();
    //}
}
