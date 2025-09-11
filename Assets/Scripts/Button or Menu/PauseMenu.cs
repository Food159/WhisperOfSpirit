using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum LevelShop
{
    Normal, Shop
}
public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public bool _isPanel;
    public LevelShop _levelShop;

    private PlayerController playercontroller;
    private PlayerShooting playershooting;
    private void Awake()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        playershooting = FindObjectOfType<PlayerShooting>();
    }
    private void Update()
    {
        if(_levelShop == LevelShop.Normal)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPanel)
                {
                    closePanel();
                }
                else
                {
                    togglePanel();
                }
            }
            if (_isPanel)
            {
                DisablePlayercontrol();
            }
            else
            {
                EnablePlayercontrol();
            }
        }
    }
    public void togglePanel()
    {
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
            _isPanel = true;
        }
    }
    public void closePanel()
    {
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            _isPanel = false;
        }
    }
    public void OnButtonRestart()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameDataHandler.instance.LoadData();
        _isPanel = false;
    }
    public void OnButtonMenu()
    {
        SceneController.instance.LoadSceneName("SceneMenu");
        Time.timeScale = 1;
    }
    public void DisablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = false;
        if (playershooting != null) playershooting.enabled = false;
    }
    public void EnablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = true;
        if (playershooting != null) playershooting.enabled = true;
    }
}
