using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State
{
    public AnimationClip jumpclip;
    public AnimationClip floatclip;
    public AnimationClip fallclip;
    public override void Enter()
    {

    }
    public override void Do()
    {
        if (rb2d.velocity.y < 0)
        {
            anim.Play(fallclip.name);
        }
        else if (Mathf.Abs(rb2d.velocity.y) < 10f)
        {
            anim.Play(floatclip.name);
        }
        else if (rb2d.velocity.y > 0)
        {
            anim.Play(jumpclip.name);
        }
        if (pinput._isGround)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
