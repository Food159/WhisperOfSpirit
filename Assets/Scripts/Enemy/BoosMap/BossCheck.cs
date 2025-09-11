using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossType
{
    Lung, Kid, Teen
}
public class BossCheck : MonoBehaviour
{
    public BossType bossType;
    [SerializeField] private EnemyHealth enemyhp;

    [Space]
    [Header("Lung")]
    [SerializeField] private GameObject pointcheck;
    [SerializeField] private PointCheck pointcheckScript;

    [Space]
    [Header("Kid")]
    [SerializeField] private GameObject iceGame;
    [SerializeField] private Ice_Water icewaterScript;

    [Space]
    [Header("Teen")]
    [SerializeField] private GameObject ipadGame;
    [SerializeField] private IpadTeen ipadScript;

    [Space]
    public bool _isBossDie = false;
    private bool hasStarted = false;
    private void Update()
    {
        if(bossType == BossType.Lung)
        {
            if (enemyhp.currentHealth <= 0 && hasStarted == false)
            {
                hasStarted = true;
                _isBossDie = true;
                pointcheck.SetActive(true);
                pointcheckScript.StartCheck();
            }
        }
        else if(bossType == BossType.Kid) 
        {
            if (enemyhp.currentHealth <= 0 && hasStarted == false)
            {
                hasStarted = true;
                _isBossDie = true;
                iceGame.SetActive(true);
                icewaterScript.StartIceGame();
            }
        }
        else if(bossType == BossType.Teen) 
        {
            if (enemyhp.currentHealth <= 0 && hasStarted == false)
            {
                hasStarted = true;
                _isBossDie = true;
                ipadGame.SetActive(true);
                ipadScript.StartRound();
            }
        }
    }
}
