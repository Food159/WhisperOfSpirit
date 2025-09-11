using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeenIdleState : TeenState
{
    public AnimationClip animclip;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {

    }
    public override void Exit()
    {

    }
}
