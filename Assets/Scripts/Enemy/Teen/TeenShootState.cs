using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeenShootState : TeenState
{
    public AnimationClip animclip;
    bool _isAnimationFinished = false;
    [SerializeField] bool _isAttacking;
    [SerializeField] LayerMask playerlayer;
    PlayerHealth playerstatus;
    private ObjectPool objectPool;
    private TeenController teenController;

    [Header("Variable")]
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 5f;
    private void Awake()
    {
        playerstatus = FindAnyObjectByType<PlayerHealth>();
        objectPool = FindObjectOfType<ObjectPool>();
        teenController = GetComponent<TeenController>();
    }
    public override void Enter()
    {
        if (playerstatus._isPlayerDead == false && teenInput.canShoot)
        {
            _isAttacking = true;
            anim.Play(animclip.name);
        }
    }
    public void TShoot()
    {
        if (objectPool == null || firePoint == null || teenController == null)
        {
            return;
        }

        GameObject bulletTeen = objectPool.GetTeenObject();
        if (bulletTeen != null)
        {
            bulletTeen.transform.position = firePoint.position;
            bulletTeen.transform.rotation = firePoint.rotation;

            Rigidbody2D rb2d = bulletTeen.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                float direction = teenController.sentDirection;
                rb2d.velocity = new Vector2(direction * bulletSpeed, 0f);

                teenInput.canShoot = false;
            }
        }
    }
    public override void Do()
    {
        if (teenInput.playerTarget.position.x > teenInput.transform.position.x && !teenInput._isFacingRight)
        {
            teenInput.Flip();
        }
        else if (teenInput.playerTarget.position.x < teenInput.transform.position.x && teenInput._isFacingRight)
        {
            teenInput.Flip();
        }
    }
    public override void Exit()
    {

    }
}
