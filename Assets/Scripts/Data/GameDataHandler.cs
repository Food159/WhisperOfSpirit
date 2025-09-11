using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ItemSpritePair
{
    public ItemsType type;
    public Sprite sprite;
}
public class GameDataHandler : MonoBehaviour
{
    [SerializeField] PlayerController playercontroller;
    [SerializeField] PlayerHealth playerhealth;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] InventoryUI inventory;

    [SerializeField] List<ItemSpritePair> itemsSpritePairs;
    Dictionary<ItemsType, Sprite> spriteLookup;
    public BossPhase phase;

    public static GameDataHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        spriteLookup = new Dictionary<ItemsType, Sprite>();
        foreach(var pair in itemsSpritePairs)
        {
            if(!spriteLookup.ContainsKey(pair.type))
            {
                spriteLookup.Add(pair.type, pair.sprite);
            }
        }
        playercontroller = FindAnyObjectByType<PlayerController>();
        playerhealth = FindAnyObjectByType<PlayerHealth>();
        scoreManager= FindAnyObjectByType<ScoreManager>();
        inventory = FindAnyObjectByType<InventoryUI>();
    }
    private void Start()
    {
        if (phase == BossPhase.phase1)
        {
            PlayerGameData gamedata = LoadData();
            if (gamedata != null)
            {
                if(playerhealth != null)
                {
                    playerhealth.currentHealth = gamedata.playerHp;
                }
                if (scoreManager != null)
                {
                    scoreManager.score = gamedata.playerScore;
                }
                foreach (ItemsType type in gamedata.inventoryItems)
                {
                    if (type != ItemsType.none && inventory != null)
                    {
                        Sprite itemSprite = GetSpriteFromType(type);
                        inventory.AddItems(itemSprite, type);
                    }
                }
            }
        }
        else if (phase == BossPhase.phase2)
        {
            PlayerGameData gamedataP2 = LoadDataBoss();
            if(gamedataP2 != null) 
            {
                if (playerhealth != null)
                {
                    playerhealth.currentHealth = gamedataP2.playerHp;
                }
                if (scoreManager != null)
                {
                    scoreManager.score = gamedataP2.playerScore;
                }
                foreach (ItemsType type in gamedataP2.inventoryItems)
                {
                    if (type != ItemsType.none && inventory != null)
                    {
                        Sprite itemSprite = GetSpriteFromType(type);
                        inventory.AddItems(itemSprite, type);
                    }
                }
            }
        } 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearData();
        }
    }

    public void SaveData()
    {
        if (phase == BossPhase.phase2)
            return;
        if(Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        PlayerGameData gamedata = new PlayerGameData();
        gamedata.playerHp = playerhealth.currentHealth;
        if(scoreManager != null)
        {
            gamedata.playerScore = scoreManager.score;
        }
        if(inventory != null)
        {
            gamedata.inventoryItems.Clear();
            for (int i = 0; i < inventory.SlotCount; i++)
            {
                gamedata.inventoryItems.Add(inventory.GetItemType(i));
            }
        }

        string gameDataJson = JsonUtility.ToJson(gamedata);
        File.WriteAllText(Application.dataPath + "/gameData.json", gameDataJson);
        Debug.Log("Save game data");
    }

    public PlayerGameData LoadData()
    {
        if (File.Exists(Application.dataPath + "/gameData.json") == false)
        {
            return null;
        }
        string loadedGameDataToJson = File.ReadAllText(Application.dataPath + "/gameData.json");
        PlayerGameData loadedGameData = JsonUtility.FromJson<PlayerGameData>(loadedGameDataToJson);
        Debug.Log("LoadData");
        return loadedGameData;
    }
    public PlayerGameData LoadDataBoss()
    {
        if (File.Exists(Application.dataPath + "/gameDataPhase1.json") == false)
        {
            return null;
        }
        string loadedGameDataToJson = File.ReadAllText(Application.dataPath + "/gameDataPhase1.json");
        PlayerGameData loadedGameData = JsonUtility.FromJson<PlayerGameData>(loadedGameDataToJson);
        Debug.Log("LoadDataPhase1");
        return loadedGameData;
    }
    public void SaveDataBoss()
    {
        if (phase == BossPhase.phase2)
            return;
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        PlayerGameData gamedata = new PlayerGameData();
        gamedata.playerHp = playerhealth.currentHealth;
        if (scoreManager != null)
        {
            gamedata.playerScore = scoreManager.score;
        }
        if (inventory != null)
        {
            gamedata.inventoryItems.Clear();
            for (int i = 0; i < inventory.SlotCount; i++)
            {
                gamedata.inventoryItems.Add(inventory.GetItemType(i));
            }
        }

        string gameDataJson = JsonUtility.ToJson(gamedata);
        File.WriteAllText(Application.dataPath + "/gameDataPhase1.json", gameDataJson);
        Debug.Log("Save game dataPhase1");
    }
    public void ClearData()
    {
        string path = Application.dataPath + "/gameData.json";
        string pathPhase1 = Application.dataPath + "/gameDataPhase1.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("deleted save");
        }
        else
        {
            Debug.Log("No data to delete");
        }
        if(File.Exists(pathPhase1))
        {
            File.Delete(pathPhase1);
            Debug.Log("deleted save phase1");
        }
        else
        {
            Debug.Log("No data to delete phase1");
        }
    }
    public void ClearDataBoss()
    {
        string pathPhase1 = Application.dataPath + "/gameDataPhase1.json";
        if (File.Exists(pathPhase1))
        {
            File.Delete(pathPhase1);
            Debug.Log("deleted save phase1");
        }
        else
        {
            Debug.Log("No data to delete phase1");
        }
    }
    private Sprite GetSpriteFromType(ItemsType type)
    {
        if (spriteLookup.TryGetValue(type, out Sprite sprite))
            return sprite;
        return null;
    }
    //public void OnApplicationQuit()
    //{
    //    SaveData();
    //}
}
