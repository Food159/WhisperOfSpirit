using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LevelWinType
{
    Menu, Tutorial, Gameplay
}
public class WinCheck : MonoBehaviour
{
    public bool wintestCheck = false;
    [SerializeField] public GameObject winpanel;
    [SerializeField] public GameObject losepanel;

    public LevelWinType levelwin;

    private PlayerController playercontroller;
    private PlayerShooting playershooting;
    private PlayerHealth playerhealth;
    PauseMenu pauseMenu;

    private EnemyHealth[] allEnemies;
    [SerializeField] PointCheck pointcheck;
    public int TotalEnemies => allEnemies.Length;
    public int DeadEnemiesCount { get; private set; }
    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        playercontroller = FindObjectOfType<PlayerController>();
        playershooting = FindObjectOfType<PlayerShooting>();
        playerhealth = FindObjectOfType<PlayerHealth>();
    }
    private void Start()
    {
        allEnemies = FindObjectsOfType<EnemyHealth>();
        EnablePlayercontrol();
    }
    private void Update()
    {
        CheckWin();
        UpdateDeadEnemies();
    }
    void UpdateDeadEnemies()
    {
        int count = 0;
        foreach(EnemyHealth enemy in allEnemies) 
        {
            if(enemy._isDead)
            {
                count++;
            }
        }
        DeadEnemiesCount = count;
    }
    void CheckWin()
    {
        if (levelwin == LevelWinType.Tutorial && !wintestCheck && DeadEnemiesCount >= TotalEnemies)
        {
            Win();
        }
    }
    public void Win()
    {
        //GameDataHandler.instance.SaveData();
        wintestCheck = true;
        SoundManager.instance.PlaySfx(SoundManager.instance.winClip);
        if (winpanel != null)
        {
            winpanel.SetActive(true);
        }
        wintestCheck = true;
        DisablePlayercontrol();
        playercontroller.CanMove();
        if (playerhealth != null) playerhealth.enabled = false;
        //if(Track.instance.trackCompleted == true) // pointcheck.isSuccess == true && 
        //{
        //    GameDataHandler.instance.SaveData();
        //    wintestCheck = true;
        //    SoundManager.instance.PlaySfx(SoundManager.instance.winClip);
        //    if (winpanel != null)
        //    {
        //        winpanel.SetActive(true);
        //    }
        //    wintestCheck = true;
        //    DisablePlayercontrol();
        //    playercontroller.CanMove();
        //}
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
