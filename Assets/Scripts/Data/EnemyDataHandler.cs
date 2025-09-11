using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyDataHandler : MonoBehaviour
{
    [SerializeField] LungController[] lungcontroller;
    [SerializeField] EnemyHealth[] enemyhealth;

    public static EnemyDataHandler instance;
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
    }
    private void Start()
    {
        EnemyData enemydata = LoadDataEnemy();
        if (enemydata != null)
        {
            for(int i = 0; i < lungcontroller.Length; i++) 
            {
                if(i < enemydata.enemyPos.Count)
                {
                    lungcontroller[i].transform.position = enemydata.enemyPos[i];
                }
            }
            for(int i = 0; i < enemyhealth.Length; i++) 
            {
                if(i < enemydata.enemyHp.Count)
                {
                    enemyhealth[i].currentHealth = enemydata.enemyHp[i];
                }
                if(i < enemydata.enemyDied.Count)
                {
                    enemyhealth[i]._isDead = enemydata.enemyDied[i];
                }
                if (enemydata.enemyDied[i])
                {
                    lungcontroller[i].gameObject.SetActive(false);
                }
            }
            //if (lungcontroller != null)
            //{
            //    lungcontroller.transform.position = enemydata.enemyPos;
            //}
            //enemyhealth.currentHealth = enemydata.enemyHp;
            //enemyhealth._isDead = enemydata.enemyDied;
            //if (enemydata.enemyDied)
            //{
            //    enemyhealth._isDead = enemydata.enemyDied;
            //    Destroy(lung);
            //}
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveDataEnemy();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearDataEnemy();
        }
    }
    public void SaveDataEnemy()
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        EnemyData enemydata = new EnemyData();
        foreach(LungController lung in lungcontroller)
        {
            enemydata.enemyPos.Add(lung.transform.position);
        }
        foreach(EnemyHealth health in enemyhealth)
        {
            enemydata.enemyHp.Add(health.currentHealth);
            enemydata.enemyDied.Add(health._isDead);
        }
        //enemydata.enemyPos = lungcontroller.transform.position;
        //enemydata.enemyHp = enemyhealth.currentHealth;
        //enemydata.enemyDied = enemyhealth._isDead;

        string enemyDataJson = JsonUtility.ToJson(enemydata);
        File.WriteAllText(Application.dataPath + "/enemyData.json", enemyDataJson);
        Debug.Log("Save enemy data");
    }
    public EnemyData LoadDataEnemy()
    {
        if (File.Exists(Application.dataPath + "/enemyData.json") == false)
        {
            return null;
        }
        string loadedEnemyDataToJson = File.ReadAllText(Application.dataPath + "/enemyData.json");
        EnemyData loadedEnemyData = JsonUtility.FromJson<EnemyData>(loadedEnemyDataToJson);
        return loadedEnemyData;
    }
    public void ClearDataEnemy()
    {
        string path = Application.dataPath + "/enemyData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("deleted save");
        }
        else
        {
            Debug.Log("No data to delete");
        }
    }
    //public void OnApplicationQuit()
    //{
    //    SaveDataEnemy();
    //}
}
