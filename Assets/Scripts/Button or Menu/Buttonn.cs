using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonn : MonoBehaviour
{
    WinCheck wincheck;
    private void Start()
    {
        wincheck = GetComponent<WinCheck>();
    }
    public void OnButtonRestartBoss()
    {
        Time.timeScale = 1;
        SceneController.instance.LoadSceneName("SceneGameBoss");
        GameDataHandler.instance.LoadData();
    }
    public void OnButtonRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameDataHandler.instance.LoadData();
    }
    public void OnButtonMenu()
    {
        SceneController.instance.LoadSceneName("SceneMenu");
        //wincheck.wintestCheck = false;
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        GameDataHandler.instance.SaveData();
        SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
        GameDataHandler.instance.LoadData();
        Debug.Log("Next");
    }
}
