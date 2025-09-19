using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float lifeTime = 4f;
    private float timer;
    private int damage = 20;
    [SerializeField] Items items;
    [SerializeField] Sprite bulletNewSprite;
    [SerializeField] Sprite bulletDefaultSprite;
    [SerializeField] SpriteRenderer spriterenderer;
    private void Awake()
    {
        items = FindAnyObjectByType<Items>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        if(items != null) 
        {
            if (items.damageIncrese)
            {
                damage = 30;
            }
            else if (!items.damageIncrese)
            {
                damage = 320;
            }
            if(items.changeSprite)
            {
                spriterenderer.sprite = bulletNewSprite;
            }
            else if(!items.changeSprite)
            {
                spriterenderer.sprite = bulletDefaultSprite;
            }
        }
        Vector2 bulletWorldToViewportPos = Camera.main.WorldToViewportPoint(transform.position);
        bool _isOutOfScreen = bulletWorldToViewportPos.x < 0 || bulletWorldToViewportPos.x > 1 || bulletWorldToViewportPos.y < 0 || bulletWorldToViewportPos.y > 1;

        timer += Time.deltaTime;
        if(timer >= lifeTime || _isOutOfScreen)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("Collided with: " + collision.gameObject.name);
            EnemyHealth enemyhealth = collision.GetComponent<EnemyHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }
        else if(collision.CompareTag("Boss"))
        {
            BossHealth bosshealth = collision.GetComponent<BossHealth>();
            if (bosshealth != null)
            {
                bosshealth.TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}
