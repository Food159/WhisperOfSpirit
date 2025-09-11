using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungWalkState : LungState
{
    public AnimationClip animclip;
    private float walkspeed = 3.5f;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        float direction = Mathf.Sign(linput.playerTarget.position.x - linput.transform.position.x);
        if(linput.transform.localScale.x > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        //rb2d.velocity = new Vector2(walkspeed * linput.sentDirection, rb2d.velocity.y);
        rb2d.velocity = new Vector2(walkspeed * direction, rb2d.velocity.y);

        if (linput.DistanceCal() <= 1.9f || linput.DistanceCal() > 10f || linput.isOutofArea)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
