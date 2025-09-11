using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidBullet : MonoBehaviour
{
    public float lifeTime = 2f;
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
            Debug.Log("Bullet Enemy Collided with: " + collision.gameObject.name);
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                camerashake.CamShaking();
            }
            gameObject.SetActive(false);
        }
    }
}
