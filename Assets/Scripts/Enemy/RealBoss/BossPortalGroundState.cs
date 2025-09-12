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
    public AnimationClip animExitRainClip;
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
        portalGroundMax = Random.Range(2, 3);
        portalGroundAmount = Random.Range(4, 6);
        transform.position = bossPortalPos.position;
        col2d.enabled = false;
        shadow.SetActive(false);
        portaling = true;
        anim.Play(animEnterClip.name);
    }
    public override void Do()
    {
        if(portaling)
        {
            timeCount += Time.deltaTime;
        }
        if(toIdle != toExit)
        {
            anim.Play(animIdleClip.name);
            toExit = true;
        }
    }
    public void BossPortalGround()
    {
        int random = Random.Range(0, portalGroundPoint.Length);
        Transform allPoint = portalGroundPoint[random];
        GameObject portalGround = objectpool.GetBossPortalGroundObject();
    }
    public override void Exit()
    {
        
    }
}
