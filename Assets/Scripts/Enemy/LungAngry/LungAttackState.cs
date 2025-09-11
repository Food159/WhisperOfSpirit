using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungAttackState : LungState
{
    public AnimationClip animclip;
    bool _canAttack = true;
    //float attackCooldown = 2f;
    bool _isAnimationFinished = false;
    public bool _isAttacking;
    [SerializeField] private int damage = 20;
    [SerializeField] Transform attackpoint;
    [SerializeField] float attackrange;
    [SerializeField] LayerMask playerlayer;
    PlayerHealth playerstatus;
    private CameraShake camerashake;

    private void Awake()
    {
        playerstatus = FindAnyObjectByType<PlayerHealth>();
        camerashake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }
    public override void Enter()
    {
        if (_canAttack && playerstatus._isPlayerDead == false)
        {
            anim.Play(animclip.name);
            _isAttacking = true;
        }
    }
    void attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, playerlayer);
        foreach (Collider2D hit in hits) 
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.lungAttackClip);
            PlayerHealth playerhealth = hit.GetComponent<PlayerHealth>();
            PlayerController playercontroller = hit.GetComponent<PlayerController>();
            if (playerhealth != null) 
            {
                playerhealth.TakeDamage(damage);
                camerashake.CamShaking();
                Debug.Log("โจมตี Player! HP ที่เหลือ: " + playerhealth.currentHealth);
            }
            if (playercontroller != null)
            {
                playercontroller.knockback(transform);
            }
        }
    }
    //IEnumerator AttackWithCooldown()
    //{
    //    _canAttack = false;
    //    attack();
    //    yield return new WaitForSeconds(attackCooldown);
    //    _canAttack = true;
    //}
    public override void Do()
    {
        rb2d.velocity = Vector2.down;
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.normalizedTime >= 1f)
        {
            _isAnimationFinished = true;
            _isAttacking = false;
        }
        if(linput.DistanceCal() > 1.9f && _isAnimationFinished)
        {
            isComplete = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
    public override void Exit()
    {

    }
}
