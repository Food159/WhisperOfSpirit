using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class KidShootState : KidState
{
    public AnimationClip animclip;
    bool _canAttack = true;
    bool _isAnimationFinished = false;
    [SerializeField] bool _isAttacking;
    [SerializeField] LayerMask playerlayer;
    PlayerHealth playerstatus;
    private ObjectPool objectPool;
    private KidController kidController;

    [Header("Variable")]
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 5f;
    private void Awake()
    {
        playerstatus = FindAnyObjectByType<PlayerHealth>();
        objectPool = FindObjectOfType<ObjectPool>();
        kidController = GetComponent<KidController>();
    }
    public override void Enter()
    {
        if (_canAttack && playerstatus._isPlayerDead == false)
        {
            _isAttacking = true;
            //_canAttack = false;
            anim.Play(animclip.name);
            //KShoot();
            
            //StartCoroutine(AttackCooldown());
        }
    }
    public void KShoot()
    {
        if(objectPool == null && firePoint == null && kidController == null)
            return;

        GameObject bulletKid = objectPool.GetKidObject();
        if(bulletKid != null)
        {
            bulletKid.transform.position = firePoint.position;
            bulletKid.transform.rotation = firePoint.rotation;

            Rigidbody2D rb2d = bulletKid.GetComponent<Rigidbody2D>();
            if(rb2d != null)
            {
                float direction = kidController.sentDirection;
                rb2d.velocity = new Vector2(direction * bulletSpeed, 0f);
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(animclip.length);
        _canAttack = true;
        _isAttacking = false;
    }
    public override void Do()
    {
        if(kidInput.playerTarget.position.x > kidInput.transform.position.x && !kidInput._isFacingRight)
        {
            kidInput.Flip();
        }
        else if(kidInput.playerTarget.position.x < kidInput.transform.position.x && kidInput._isFacingRight)
        {
            kidInput.Flip();
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
    }
    public override void Exit()
    {

    }
}
