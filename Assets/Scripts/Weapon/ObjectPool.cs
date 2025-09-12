using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("PLayer")]
    public GameObject bulletPrefabs;
    public int poolSize = 5;
    private List<GameObject> pool;

    [Space]
    [Header("Enemy Kid")]
    public GameObject kidBulletsPrefabs;
    public int kidPoolSize = 5;
    private List<GameObject> kidPool;
    [SerializeField] private KidController kidcontroller;

    [Space]
    [Header("Enemy Teen")]
    public GameObject[] teenBulletsPrefabs;
    public int teenPoolSize = 1;
    private List<GameObject> teenPool;
    [SerializeField] private TeenController teencontroller;

    [Space]
    [Header("Boss")]
    [Header("Portal")]
    public GameObject bossBulletsPrefabs;
    public GameObject portalPrefabs;
    public int portalPoolSize = 6;
    public int bossPoolSize = 6;
    private List<GameObject> bossPool;
    private List<GameObject> portalPool;

    [Header("Rain")]
    public GameObject bossRainPrefabs;
    public int rainPoolSize = 20;
    private List<GameObject> rainPool;

    [Header("Portal Ground")]
    public GameObject portalGroundPrefabs;
    public int portalGroundPoolSize = 6;
    private List<GameObject> portalGroundPool;

    [Header("O")]
    [SerializeField] private BossController bossController;

    private void Awake()
    {
        kidcontroller = FindObjectOfType<KidController>();
        teencontroller = FindObjectOfType<TeenController>();
        bossController = FindObjectOfType<BossController>();
    }
    private void Start()
    {
        pool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
        if(kidcontroller != null)
        {
            kidPool = new List<GameObject>(); //if have kid
            for (int j = 0; j < kidPoolSize; j++)
            {
                GameObject kidBullet = Instantiate(kidBulletsPrefabs);
                kidBullet.SetActive(false);
                kidPool.Add(kidBullet);
            }
        }
        if (teencontroller != null && teenBulletsPrefabs.Length > 0)
        {
            teenPool = new List<GameObject>(); //if have teen
            for (int k = 0; k < teenPoolSize; k++)
            {
                for(int u = 0; u < teenBulletsPrefabs.Length; u++)
                {
                    GameObject teenBullet = Instantiate(teenBulletsPrefabs[u]);
                    teenBullet.SetActive(false);
                    teenPool.Add(teenBullet);
                }
            }
        }
        if(bossController != null) 
        {
            bossPool = new List<GameObject>(); //if have boss
            for(int h = 0; h < bossPoolSize; h++)
            {
                GameObject bossBullet = Instantiate(bossBulletsPrefabs);
                bossBullet.SetActive(false);
                bossPool.Add(bossBullet);
            }
            portalPool = new List<GameObject>();
            for(int h = 0; h < portalPoolSize; h++)
            {
                GameObject portal = Instantiate(portalPrefabs);
                portal.SetActive(false);
                portalPool.Add(portal);
            }
            rainPool = new List<GameObject>();
            for(int h = 0; h < rainPoolSize; h++)
            {
                GameObject rain = Instantiate(bossRainPrefabs);
                rain.SetActive(false);
                rainPool.Add(rain);
            }
            portalGroundPool = new List<GameObject>();
            for (int h = 0; h < portalGroundPoolSize; h++)
            {
                GameObject portalGround = Instantiate(portalGroundPrefabs);
                portalGround.SetActive(false);
                portalGroundPool.Add(portalGround);
            }
        }
    }
    public void BossBullet()
    {

    }
    public GameObject GetObject()
    {
        foreach(GameObject bullet in pool)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }
    public GameObject GetKidObject()
    {
        foreach(GameObject bulletKid in kidPool)
        {
            if(!bulletKid.activeInHierarchy)
            {
                bulletKid.SetActive(true);
                return bulletKid;
            }
        }
        return null;
    }
    public GameObject GetTeenObject()
    {
        int randomIndex = Random.Range(0, teenBulletsPrefabs.Length);
        GameObject prefabUse = teenBulletsPrefabs[randomIndex];

        foreach (GameObject bulletteen in teenPool)
        {
            if (!bulletteen.activeInHierarchy && bulletteen.name.Contains(prefabUse.name))
            {
                bulletteen.SetActive(true);
                return bulletteen;
            }
        }
        return null;
    }
    public GameObject GetBossObject()
    {
        foreach (GameObject portal in portalPool)
        {
            if (!portal.activeInHierarchy)
            {
                portal.SetActive(true);
                return portal;
            }
        }
        return null;
    }
    public GameObject GetBossPortalGroundObject()
    {
        foreach (GameObject portalGround in portalGroundPool)
        {
            if (!portalGround.activeInHierarchy)
            {
                portalGround.SetActive(true);
                return portalGround;
            }
        }
        return null;
    }
    public GameObject GetBossBulletObject()
    {
        foreach (GameObject bulletBoss in bossPool)
        {
            if (!bulletBoss.activeInHierarchy)
            {
                bulletBoss.SetActive(true);
                return bulletBoss;
            }
        }
        return null;
    }
    public GameObject GetBossRainObject()
    {
        foreach(GameObject rainBoss in rainPool)
        {
            if(!rainBoss.activeInHierarchy)
            {
                rainBoss.SetActive(true);
                return rainBoss;
            }
        }
        return null;
    }
}
