using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeenWalkState : TeenState
{
    public AnimationClip animclip;
    private float walkspeed = 3.5f;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        Vector3 target = teenInput.currentTargetPos;
        float direction = Mathf.Sign(target.x - teenInput.transform.position.x);

        if (direction > 0 && !teenInput._isFacingRight)
        {
            teenInput.Flip();
        }
        else if (direction < 0 && teenInput._isFacingRight)
        {
            teenInput.Flip();
        }
        rb2d.velocity = new Vector2(walkspeed * direction, rb2d.velocity.y);

        if (Mathf.Abs(teenInput.transform.position.x - target.x) <= 0.1f)
        {
            if (teenInput.currentTargetPos == teenInput.Area_posX.position)
            {
                teenInput.currentTargetPos = teenInput.Area_negX.position;
            }
            else
            {
                teenInput.currentTargetPos = teenInput.Area_posX.position;
            }
        }
        if (teenInput.isOutofArea)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
