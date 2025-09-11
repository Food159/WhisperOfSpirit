using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRainState : BossState
{
    public AnimationClip animclip;
    public AnimationClip animEnterClip;
    public AnimationClip animExitClip;
    public AnimationClip animExitRainClip;
    public AnimationClip animIdleClip;
    public CapsuleCollider2D col2d;
    public bool raining;
    public Transform bossRainPos;
    public float timeCount;
    [SerializeField] GameObject shadow;
    [SerializeField] Transform[] RainPoint;
    private ObjectPool objectpool;
    public bool toIdle = false;
    public bool toExit = false;
    public BossPhase phase;

    [Header("Rain Settings")]
    public float rainInterval;
    [SerializeField] private int rainAmount = 5;
    public float rainDuration;
    private float timer = 0f;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
    }
    public override void Enter()
    {
        rainDuration = Random.Range(9f, 20f);
        transform.position = bossRainPos.position;
        col2d.enabled = false;
        shadow.SetActive(false);
        raining = true;
        anim.Play(animEnterClip.name);

        StartCoroutine(RainEnter());
    }
    public override void Do()
    {
        if(raining)
        {
            timeCount += Time.deltaTime;
        }
        if(toIdle && !toExit)
        {
            anim.Play(animIdleClip.name);
            toExit = true;
        }
    }
    public override void Exit()
    {
        transform.position = bossInput.startBossPos.position;
        toIdle = false;
        col2d.enabled = true;
    }
    IEnumerator Rain()
    {
        while (timer < rainDuration)
        {
            for (int i = 0; i < rainAmount; i++)
            {
                float randomP = Random.Range(-14f, 12f);
                Vector2 spawnPos = new Vector2(randomP, 11.28f);
                GameObject rainspawn = objectpool.GetBossRainObject();
                if (rainspawn != null)
                {
                    rainspawn.transform.position = spawnPos;
                    rainspawn.SetActive(true);
                }
            }

            float intervalTimer = 0f;
            while (intervalTimer < rainInterval) // phase 1 interval 0.2 phase 2 0.1
            {
                intervalTimer += Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        anim.Play(animExitClip.name);
        StartCoroutine(RainExit());


        bossInput.canShoot = false;
        shadow.SetActive(true);
    }
    IEnumerator RainEnter()
    {
        yield return new WaitForSeconds(animEnterClip.length);
        timeCount = 0;

        if (phase == BossPhase.phase1)
        {
            rainInterval = 0.2f;
        }
        else if (phase == BossPhase.phase2)
        {
            rainInterval = 0.1f;
        }
        anim.Play(animclip.name);
        yield return new WaitForSeconds(animclip.length);
        toIdle = true;
        StartCoroutine(Rain());
    }
    IEnumerator RainExit()
    {
        yield return new WaitForSeconds(animExitClip.length);
        //raining = false;
        Exit();
        yield return StartCoroutine(bossInput.DelayRainBeforeFire());
    }
}
