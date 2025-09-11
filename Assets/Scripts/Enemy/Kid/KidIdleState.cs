using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidIdleState : KidState
{
    public AnimationClip animclip;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        //rb2d.velocity = Vector2.zero;
    }
    public override void Exit()
    {

    }
}
