using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPortalGround : MonoBehaviour
{
    private ObjectPool objectpool;
    [SerializeField] float bulletSpeed;
    BossFireState bossFire;
    BossController bossController;
    [SerializeField] Transform firePoint;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
        bossFire = FindObjectOfType<BossFireState>();
        bossController = FindObjectOfType<BossController>();
    }
    private void Update()
    {
        if (bossFire != null)
        {
            bulletSpeed = 15f;
        }
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
        }
    }
}
