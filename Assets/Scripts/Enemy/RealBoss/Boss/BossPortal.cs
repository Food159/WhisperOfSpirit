using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BossPortal : MonoBehaviour
{
    private ObjectPool objectpool;
    [SerializeField] float bulletSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    BossFireState bossFire;
    BossController bossController;
    [SerializeField] Transform firePoint;
    public bool isLeft;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
        bossFire = FindObjectOfType<BossFireState>();
        bossController = FindObjectOfType<BossController>();
    }
    private void Update()
    {
        if(bossFire != null) 
        {
            if(!bossFire.phase2)
            {
                bulletSpeed = 10f;
            }
            else if(bossFire.phase2)
            {
                bulletSpeed = 12f;
            }
        }
    }
    public void ClosePortal()
    {
        gameObject.SetActive(false);
    }
    public void SetDirection(bool left)
    {
        isLeft = left;
        if(spriteRenderer != null) 
        {
            spriteRenderer.flipX = isLeft;
        }
        Vector2 pos = firePoint.localPosition;
        if (isLeft)
        {
            pos.x = 0.5f; /*Mathf.Abs(pos.x);*/
        }
        else
        {
            pos.x = -0.5f; /*-Mathf.Abs(pos.x);*/
        }
        firePoint.localPosition = pos;
    }
    public void BossShoot()
    {
        GameObject bossBullet = objectpool.GetBossBulletObject();
        if (bossBullet != null)
        {
            bossBullet.transform.position = firePoint.position;
            Rigidbody2D rb2d = bossBullet.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                if(isLeft)
                {
                    rb2d.velocity = Vector2.right * bulletSpeed;
                }
                else if(!isLeft)
                {
                    rb2d.velocity = Vector2.left * bulletSpeed;
                }
                bossController.currentAttackCount++;
            }
        }
    }
}
