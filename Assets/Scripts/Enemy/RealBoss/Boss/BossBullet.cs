using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BossBullet : MonoBehaviour
{
    public float lifeTime = 6f;
    private float timer;

    private CameraShake camerashake;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Awake()
    {
        camerashake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Bullet Boss Collided with: " + collision.gameObject.name);
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(40);
                camerashake.CamShaking();
            }
            gameObject.SetActive(false);
        }
    }
}
