using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
public enum BossPhase
{
    phase1, phase2
}
public class BossFireState : BossState
{
    public AnimationClip animclip;
    public AnimationClip animExitClip;
    PlayerHealth playerStatus;
    public BossPhase bossphase;
    private ObjectPool objectpool;
    private BossController bossController;
    public bool exitToRain = false;
    public bool phase2 = false;

    [Space]
    [Header("Variable")]
    //private float attackSpeed = 3;

    public bool _isAttacking;
    [SerializeField] Transform[] portalPoint;
    
    private void Awake()
    {
        //exitToRain = false;
        playerStatus = FindAnyObjectByType<PlayerHealth>();
        objectpool = FindObjectOfType<ObjectPool>();
        bossController = GetComponent<BossController>();
    }
    public override void Enter()
    {
        //exitToRain = false;
        if (playerStatus._isPlayerDead == false)
        {
            anim.Play(animclip.name);

        }
    }
    public void CantShoot()
    {
        bossController.canShoot = false;
    }
    public void BossPortal()
    {
        if(bossphase == BossPhase.phase1) 
        {
            phase2 = false;
            //int[] indexes = { 0, 2, 4 };
            //int randomindex = indexes[Random.Range(0, indexes.Length)];
            //Transform allPoint = portalPoint[randomindex];
            Transform allPoint = portalPoint[Random.Range(0, 2)];
            GameObject portal = objectpool.GetBossObject();
            if (portal != null)
            {
                portal.transform.position = allPoint.position;
                portal.transform.rotation = allPoint.rotation;
                BossPortal bossportal = portal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    if (allPoint == portalPoint[0] || allPoint == portalPoint[2] || allPoint == portalPoint[4])
                    {
                        bossportal.SetDirection(true);
                    }
                    else
                    {
                        bossportal.SetDirection(false);
                    }
                }
                
            }
        }
        else if (bossphase == BossPhase.phase2)
        {
            phase2 = true;
            int[] leftindex = { 0, 2, 4 };
            int leftrandom = leftindex[Random.Range(0, leftindex.Length)];
            Transform leftPoint = portalPoint[leftrandom];
            //Transform leftPoint = portalPoint[Random.Range(0, 3)];
            //Transform rightPoint = portalPoint[Random.Range(3, 6)];
            GameObject leftPortal = objectpool.GetBossObject();
            if (leftPortal != null)
            {
                leftPortal.transform.position = leftPoint.position;
                leftPortal.transform.rotation = leftPoint.rotation;
                BossPortal bossportal = leftPortal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    bossportal.SetDirection(true);
                }
            }
            int[] rightindex = {1, 3, 5};
            int rightrandom = rightindex[Random.Range(0, rightindex.Length)];
            Transform rightPoint = portalPoint[rightrandom];
            GameObject rightPortal = objectpool.GetBossObject();
            if (rightPortal != null)
            {
                rightPortal.transform.position = rightPoint.position;
                rightPortal.transform.rotation = rightPoint.rotation;
                BossPortal bossportal = rightPortal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    bossportal.SetDirection(false);
                }
            }
        }

    }
    public override void Do()
    {
        if(bossController.currentAttackCount >= bossController.attackCount && !bossInput.exitToState) //!exitToRain
        {
            Exit();
        }
    }
    public override void Exit()
    {
        //anim.Play(animExitClip.name);
        //Debug.Log("exitFire");
        //bossController.StartCoroutine(ExitToRain());
        if (!bossInput.exitToState)   /*(!exitToRain)*/
        {
            bossController.StartCoroutine(ExitRoutine());
        }
    }
    IEnumerator ExitToRain()
    {
        yield return new WaitForSeconds(animExitClip.length);
        //exitToRain = true;
        bossInput.exitToState = true;
    }
    IEnumerator ExitRoutine()
    {
        anim.Play(animExitClip.name);
        Debug.Log("exitFire");
        yield return new WaitForSeconds(animExitClip.length);
        //exitToRain = true;
        bossInput.exitToState = true;
    }
}
