using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossController : MonoBehaviour
{
    [Header("FSM")]
    public BossIdleState bossIdleState;
    public BossFireState bossFireState;
    public BossRainState bossRainState;
    public BossPortalGroundState bossPortalGroundState;
    public BossHappyState bossHappyState;
    public BossState state;

    [Space]
    [Header("Variable")]
    Rigidbody2D rb2d;
    Animator anim;
    public bool _isFacingRight = false;
    public Transform playerTarget;
    public Transform startBossPos;
    public bool rain = false;
    public bool portalGround = false;
    public bool exitToState = false;

    [Space]
    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Space]
    [Header("Settings")]
    public bool isAleart;
    public bool canShoot = true;
    public float shootTimer;
    private float shootDuration = 3f;
    public int attackCount;
    public int currentAttackCount;

    [Space]
    [Header("Phase")]
    public BossPhase phase;

    BossHealth status;
    public void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            Debug.Log("player not found");
        }

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = GetComponent<BossHealth>();
    }
    private void Start()
    {
        transform.position = startBossPos.position;

        if(phase == BossPhase.phase1)
        {
            attackCount = Random.Range(3, 6);
        }
        else if(phase == BossPhase.phase2)
        {
            attackCount = Random.Range(6, 12);
        }
        bossIdleState.Setup(rb2d, anim, this);
        bossFireState.Setup(rb2d, anim, this);
        bossRainState.Setup(rb2d, anim, this);
        bossPortalGroundState.Setup(rb2d, anim, this);
        bossHappyState.Setup(rb2d, anim, this);
        state = bossIdleState;
    }
    private void Update()
    {
        if(phase == BossPhase.phase2)
        {
            Check();
        }
        SelectState();
        state.Do();
    }
    void SelectState()
    {
        if (status.isDead)
            return;
        if (bossRainState.raining)
            return;
        if (bossPortalGroundState.portaling)
            return;
        if(canShoot && currentAttackCount < attackCount) 
        {
            shootTimer = 0f;
            if(state != bossFireState)
            state = bossFireState;
            state.Enter();

            return;
        }
        else if(!canShoot && currentAttackCount < attackCount) 
        {
            if (state != bossIdleState)
            {
                state = bossIdleState;
                state.Enter();
            }
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDuration)
            {
                canShoot = true;
            }

        }
        if (currentAttackCount >= attackCount && !bossRainState.raining && !bossPortalGroundState.portaling)
        {
            if (exitToState)/*(bossFireState.exitToRain)*/
            {
                exitToState = false;        //bossFireState.exitToRain = false;
                if(phase == BossPhase.phase1)
                {
                    state = bossRainState;
                    currentAttackCount = 0;
                    canShoot = false;
                    exitToState = false;
                    //bossFireState.exitToRain = false;
                    state.Enter();
                }
                if (phase == BossPhase.phase2)
                {
                    int randomState = Random.Range(0, 2);
                    if(randomState == 0)
                    {
                        rain = true;
                        portalGround = false;
                    }
                    else if(randomState == 1)
                    {
                        rain = false;
                        portalGround = true;
                    }
                    if(portalGround)
                    {
                        state = bossPortalGroundState;
                        currentAttackCount = 0;
                        canShoot = false;
                        exitToState = false;
                        //bossFireState.exitToRain = false;
                        state.Enter();
                    }
                    else if(rain)
                    {
                        state = bossRainState;
                        currentAttackCount = 0;
                        canShoot = false;
                        exitToState = false;
                        //bossFireState.exitToRain = false;
                        state.Enter();
                    }
                }
            }
        }
    }
    void Check()
    {
        if(status.isDead && state != bossHappyState)
        {
            state = bossHappyState;
            state.Enter();
        }
    }
    public IEnumerator DelayRainBeforeFire()
    {
        anim.Play(bossRainState.animExitRainClip.name);
        yield return new WaitForSeconds(bossRainState.animExitRainClip.length);
        bossRainState.raining = false;
        bossRainState.timer = 0f;
        exitToState = false;
        state = bossIdleState;
        state.Enter();
        yield return new WaitForSeconds(3f);
        currentAttackCount = 0;
        if (phase == BossPhase.phase1)
        {
            attackCount = Random.Range(3, 6);
        }
        else if (phase == BossPhase.phase2)
        {
            attackCount = Random.Range(6, 12);
        }
        canShoot = true;
    }
    public IEnumerator DelayPortalGroundBeforeFire()
    {
        anim.Play(bossPortalGroundState.animExitPortalGroundClip.name);
        yield return new WaitForSeconds(bossPortalGroundState.animExitPortalGroundClip.length);
        bossPortalGroundState.portaling = false;
        exitToState = false;
        state = bossIdleState;
        state.Enter();
        yield return new WaitForSeconds(3f);
        currentAttackCount = 0;
        if (phase == BossPhase.phase1)
        {
            attackCount = Random.Range(3, 6);
        }
        else if (phase == BossPhase.phase2)
        {
            attackCount = Random.Range(6, 12);
        }
        canShoot = true;
    }
    public void Pause()
    {
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }
}
