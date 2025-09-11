using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    float time => Time.time - startTime;
    protected Rigidbody2D rb2d;
    protected Animator anim;

    public PlayerController pinput;

    public virtual void Enter()
    {

    }
    public virtual void Do()
    {

    }
    public virtual void Exit()
    {

    }
    public void Setup(Rigidbody2D _rb2d, Animator _anim, PlayerController _playercontroller)
    {
        rb2d = _rb2d;
        anim = _anim;
        pinput = _playercontroller;
    }
    public void initialise()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
