using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class KidWalkState : KidState
{
    public AnimationClip animclip;
    private float walkspeed = 3.5f;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        Vector3 target = kidInput.currentTargetPos;
        float direction = Mathf.Sign(target.x - kidInput.transform.position.x);

        if(direction > 0 && !kidInput._isFacingRight)
        {
            kidInput.Flip();
        }
        else if(direction < 0 && kidInput._isFacingRight)
        {
            kidInput.Flip();
        }
        rb2d.velocity = new Vector2(walkspeed * direction, rb2d.velocity.y);
        
        if(Mathf.Abs(kidInput.transform.position.x - target.x) <= 0.1f)
        {
            if(kidInput.currentTargetPos == kidInput.Area_posX.position)
            {
                kidInput.currentTargetPos = kidInput.Area_negX.position;
            }
            else
            {
                kidInput.currentTargetPos = kidInput.Area_posX.position;
            }
        }
        if(kidInput.isOutofArea)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
