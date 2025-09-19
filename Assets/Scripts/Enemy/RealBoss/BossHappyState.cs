using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHappyState : BossState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public GameObject boss;
    [SerializeField] private WinCheck win;
    private void Awake()
    {
        if (win == null)
        {
            win = FindAnyObjectByType<WinCheck>();
        }
    }
    public override void Enter()
    {
        bossInput.bossHappy = true;
        Debug.Log("Hpp");
        anim.Play(animclip.name);
        col2d.enabled = false;
        StartCoroutine(WaitForHappy());
    }
    public override void Do()
    {
        //col2d.enabled = false;
        //StartCoroutine(WaitForHappy());
    }
    public override void Exit()
    {

    }
    IEnumerator WaitForHappy()
    {
        yield return new WaitForSeconds(animclip.length);
        win.Win();
        //boss.SetActive(false);
    }
}
