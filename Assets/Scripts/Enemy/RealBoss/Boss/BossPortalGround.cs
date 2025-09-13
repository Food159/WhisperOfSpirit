using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPortalGround : MonoBehaviour
{
    private ObjectPool objectpool;
    [SerializeField] float bulletSpeed = 15f;
    [SerializeField] Transform firePoint;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
    }
    public void ClosePortal()
    {
        gameObject.SetActive(false);
    }
    public void BossPortalGroundPortal()
    {
        GameObject bossBullet = objectpool.GetBossBulletObject();
        if (bossBullet != null)
        {
            bossBullet.transform.position = firePoint.position;
            Rigidbody2D rb2d = bossBullet.GetComponent<Rigidbody2D>();
            if(rb2d != null ) 
            {
                rb2d.velocity = Vector2.up * bulletSpeed;
            }
        }
    }
}
