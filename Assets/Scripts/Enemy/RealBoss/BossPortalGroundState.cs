using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Pool;

public class BossPortalGroundState : BossState
{
    public AnimationClip animAttackclip;
    public AnimationClip animEnterClip;
    public AnimationClip animExitClip;
    public AnimationClip animExitPortalGroundClip;
    public AnimationClip animIdleClip;
    public CapsuleCollider2D col2d;
    public bool portaling;
    public Transform bossPortalPos;
    public float timeCount;
    [SerializeField] GameObject shadow;
    [SerializeField] Transform[] portalGroundPoint;
    private ObjectPool objectpool;
    public bool toIdle = false;
    public bool toExit = false;
    public BossPhase phase;
    public int portalGroundAmount; // จำนวน portal ที่ spawn
    public int currentPortalGround; // นับจำนวนรอบ
    public int portalGroundMax; // จำนวนรอบ
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
    }
    public override void Enter()
    {
        portalGroundMax = Random.Range(3, 5);
        portalGroundAmount = Random.Range(8, 10);
        transform.position = bossPortalPos.position;
        col2d.enabled = false;
        shadow.SetActive(false);
        portaling = true;
        anim.Play(animEnterClip.name);

        StartCoroutine(PortalGroundRoutine());
    }
    public override void Do()
    {
        if(portaling)
        {
            timeCount += Time.deltaTime;
        }
        if(toIdle && !toExit)
        {
            anim.Play(animIdleClip.name);
            toExit = true;
        }
    }
    public void BossPortalGround()
    {
        List<Transform> availablePoints = new List<Transform>(portalGroundPoint);
        int spawnCount = Mathf.Min(portalGroundAmount, availablePoints.Count);
        for(int i = 0; i < spawnCount; i++) 
        {
            int random = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[random];
            GameObject portalGround = objectpool.GetBossPortalGroundObject();
            if (portalGround != null)
            {
                portalGround.transform.position = spawnPoint.position;
                portalGround.SetActive(true);
            }
            availablePoints.RemoveAt(random);
        }
        currentPortalGround++;
    }
    public override void Exit()
    {
        transform.position = bossInput.startBossPos.position;
        col2d.enabled = true;
    }
    //IEnumerator PortalGroundEnter()
    //{
    //    yield return new WaitForSeconds(animEnterClip.length);
    //    timeCount = 0;

    //    anim.Play(animAttackclip.name);
    //    yield return new WaitForSeconds(animAttackclip.length);
    //    toIdle = true;
    //}
    IEnumerator PortalGroundRoutine()
    {
        yield return new WaitForSeconds(animEnterClip.length);
        timeCount = 0;
        for(int i = 0; i < portalGroundMax; i++)
        {
            anim.Play(animAttackclip.name);
            Debug.Log("Atk");
            yield return new WaitForSeconds(animAttackclip.length);
            if(i < portalGroundMax - 1) 
            {
                anim.Play(animIdleClip.name);
                Debug.Log("Idle");
                yield return new WaitForSeconds(3f);
            }
        }
        anim.Play(animIdleClip.name);
        yield return new WaitForSeconds(4f);
        anim.Play(animExitClip.name);
        StartCoroutine(PortalGroundExit());


        bossInput.canShoot = false;
        shadow.SetActive(true);
    }
    IEnumerator PortalGroundExit()
    {
        yield return new WaitForSeconds(animExitClip.length);
        Exit();
        yield return StartCoroutine(bossInput.DelayPortalGroundBeforeFire());
    }
}
