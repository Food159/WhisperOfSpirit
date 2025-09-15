using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : Subject, IPausable
{
    public ObjectPool bulletpool;

    public Transform shootingPoint;
    //public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int waterammo;
    private float waterReload = 0.9f;
    public float waterTestReload;

    private bool _isFacingRight = true;
    SoundManager soundmanager;
    PauseMenu pausemenu;
    PlayerHealth status;
    PlayerWater playerwater;
    [SerializeField] Items items;
    private void Awake()
    {
        playerwater = GetComponent<PlayerWater>();
        status = GetComponent<PlayerHealth>();
        items = FindAnyObjectByType<Items>();
        //soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>(); เอาเเพิ่มด้วยยยยยยยยยยยยยยยยย
    }
    private void Start()
    {
        StartCoroutine(WaterReload());
    }
    void Update()
    {
        waterTestReload = waterReload;
        if (status._isPlayerDead)
            return;
        if (Input.GetAxis("Horizontal") > 0)
        {
            _isFacingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _isFacingRight = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                //NotifyObservers(PlayerAction.Attack);
                shooting();
            }
        }
        if (items != null)
        {
            if (items.reloadSpeed)
            {
                waterReload = 0.9f * 0.5f;
            }
            else if (!items.reloadSpeed)
            {
                waterReload = 0.9f;
            }
        }
    }
    IEnumerator WaterReload()
    {
        while(!status._isPlayerDead) 
        {
            if(playerwater.currentWater < playerwater.maxWater)
            {
                playerwater.currentWater++;
                playerwater.waterbar.SetWater(playerwater.currentWater);
            }
            yield return new WaitForSeconds(waterReload);
        }
    }
    private void shooting()
    {
        if (playerwater.currentWater > 0)
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.tawanShootWaterClip);
            playerwater.shoot(1);
            GameObject bullet = bulletpool.GetObject();
            if(bullet != null ) 
            {
                bullet.transform.position = shootingPoint.position;
            }
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            float direction;
            {
                if (_isFacingRight)
                {
                    direction = 1f;
                }
                else
                {
                    direction = -1f;
                }
            }
            Vector2 bulletScale = bullet.transform.localScale;
            bulletScale.x = Mathf.Abs(bulletScale.x) * direction;
            bullet.transform.localScale = bulletScale;

            if (bulletRb != null)
            {
                bulletRb.velocity = new Vector2(direction * bulletSpeed, 0f);
            }
        }

    }
    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Attack):
                shooting();
                Debug.Log("Shoot Observer");
                return;
            default:
                return;
        }
    }
    private void OnEnable()
    {
        
    }
    public void Pause()
    {
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }
}
