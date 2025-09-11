using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaintState : State
{
    public AnimationClip animclip;
    public override void Enter()
    {
            anim.Play(animclip.name);
    }
    public override void Do()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play(animclip.name, 0, 1f);
    }
    public override void Exit()
    {

    }
}
