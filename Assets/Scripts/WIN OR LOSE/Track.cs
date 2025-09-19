using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum LevelTrack
{
    Game1, Game2, Game3, Game4, GameBoss
}
public class Track : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] WinCheck winCheck;
    [SerializeField] WinPlate winPlate;
    [SerializeField] BossController bossController;

    [Space]
    [Header("Track 1")]
    [SerializeField] Image trackCheck;
    [SerializeField] Sprite trackComplete;
    [SerializeField] Sprite trackIncomplete;
    [SerializeField] TMP_Text trackText;
    public bool trackCompleted;

    [Space]
    [Header("Track 2")]
    [SerializeField] Image trackCheck2;
    [SerializeField] Sprite trackComplete2;
    [SerializeField] Sprite trackIncomplete2;
    [SerializeField] TMP_Text trackText2;
    public bool trackCompleted2;

    [Space]
    [Header("Track 3")]
    [SerializeField] Image trackCheck3;
    [SerializeField] Sprite trackComplete3;
    [SerializeField] Sprite trackIncomplete3;
    [SerializeField] TMP_Text trackText3;
    public bool trackCompleted3;

    [Space]
    [Header("Variable")]
    public LevelTrack leveltrack;
    [SerializeField] PointCheck minigame1;
    [SerializeField] Ice_Water minigame2;
    [SerializeField] IpadTeen minigame3;

    public static Track instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if(bossController == null)
        {
            bossController = FindAnyObjectByType<BossController>();
        }
    }
    private void Update()
    {
        if(leveltrack == LevelTrack.Game1) 
        {
            if (winCheck == null) return;

            #region track1
            int dead = winCheck.DeadEnemiesCount;
            int total = winCheck.TotalEnemies;
            trackText.text = $"Enemies Defeated: {dead}/{total}";
            if (dead >= total)
            {
                trackCheck.sprite = trackComplete;
                trackCompleted = true;
            }
            else
            {
                trackCheck.sprite = trackIncomplete;
                trackCompleted = false;
            }
            #endregion track1
        }
        else if(leveltrack == LevelTrack.Game2) 
        {
            if (winCheck == null && winPlate == null) 
                return;

            #region track1
            trackText.text = "Tawan arrives the destinated area";
            if(winPlate.plateWin)
            {
                trackCheck.sprite = trackComplete;
                trackCompleted = true;
            }
            else
            {
                trackCheck.sprite = trackIncomplete;
                trackCompleted = false;
            }
            #endregion track1

            #region track2
            int dead = winCheck.DeadEnemiesCount;
            trackText2.text = $"Enemies Defeated: {dead}/{9}";
            if(dead >= 9)
            {
                trackCheck2.sprite = trackComplete2;
                trackCompleted2 = true;
            }
            else
            {
                trackCheck2.sprite = trackIncomplete2;
                trackCompleted2 = false;
            }
            #endregion track2

            #region track3
            trackText3.text = "Complete \"Special\" Mission";
            if(minigame1.isSuccess)
            {
                trackCheck3.sprite = trackComplete3;
                trackCompleted3 = true;
            }
            else
            {
                trackCheck3.sprite = trackIncomplete3;
                trackCompleted3 = false;
            }
            #endregion track3
        }
        else if(leveltrack == LevelTrack.Game3)
        {
            if (winCheck == null && winPlate == null) 
                return;

            #region track1
            trackText.text = "Tawan arrives the destinated area";
            if (winPlate.plateWin)
            {
                trackCheck.sprite = trackComplete;
                trackCompleted = true;
            }
            else
            {
                trackCheck.sprite = trackIncomplete;
                trackCompleted = false;
            }
            #endregion track1

            #region track2
            int dead = winCheck.DeadEnemiesCount;
            trackText2.text = $"Enemies Defeated: {dead}/{10}";
            if (dead >= 10)
            {
                trackCheck2.sprite = trackComplete2;
                trackCompleted2 = true;
            }
            else
            {
                trackCheck2.sprite = trackIncomplete2;
                trackCompleted2 = false;
            }
            #endregion track2

            #region track3
            trackText3.text = "Complete \"Special\" Mission";
            if (minigame2.success)
            {
                trackCheck3.sprite = trackComplete3;
                trackCompleted3 = true;
            }
            else
            {
                trackCheck3.sprite = trackIncomplete3;
                trackCompleted3 = false;
            }
            #endregion track3
        }
        else if(leveltrack == LevelTrack.Game4)
        {
            if (winCheck == null && winPlate == null) 
                return;

            #region track1
            trackText.text = "Tawan arrives the destinated area";
            if (winPlate.plateWin)
            {
                trackCheck.sprite = trackComplete;
                trackCompleted = true;
            }
            else
            {
                trackCheck.sprite = trackIncomplete;
                trackCompleted = false;
            }
            #endregion track1

            #region track2
            int dead = winCheck.DeadEnemiesCount;
            trackText2.text = $"Enemies Defeated: {dead}/{15}";
            if (dead >= 15)
            {
                trackCheck2.sprite = trackComplete2;
                trackCompleted2 = true;
            }
            else
            {
                trackCheck2.sprite = trackIncomplete2;
                trackCompleted2 = false;
            }
            #endregion track2

            #region track3
            trackText3.text = "Complete \"Special\" Mission";
            if (minigame3.success)
            {
                trackCheck3.sprite = trackComplete3;
                trackCompleted3 = true;
            }
            else
            {
                trackCheck3.sprite = trackIncomplete3;
                trackCompleted3 = false;
            }
            #endregion track3
        }
        if (leveltrack == LevelTrack.GameBoss)
        {
            if (winCheck == null) return;

            #region track1
            trackText.text = "Defeat Crimson Queen";
            if (bossController.bossHappy)
            {
                trackCheck.sprite = trackComplete;
                trackCompleted = true;
            }
            else
            {
                trackCheck.sprite = trackIncomplete;
                trackCompleted = false;
            }
            #endregion track1
        }
    }
}
