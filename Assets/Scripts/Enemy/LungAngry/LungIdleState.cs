using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungIdleState : LungState
{
    public AnimationClip animclip;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        //if (!pinput._isGround || linput.playerInputX != 0)
        //{
        //    isComplete = true;
        //}
    }
    public override void Exit()
    {

    }
}
