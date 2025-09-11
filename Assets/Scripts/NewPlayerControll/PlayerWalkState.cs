using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : State
{
    public AnimationClip animclip;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        if(!pinput._isGround)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
