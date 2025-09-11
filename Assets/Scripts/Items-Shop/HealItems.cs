using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItems : MonoBehaviour
{
    [SerializeField] PlayerHealth playerhealth;
    //[SerializeField] HealthBar hpbar;
    private void Awake()
    {
        playerhealth = FindAnyObjectByType<PlayerHealth>();
        //hpbar = FindAnyObjectByType<HealthBar>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerhealth.currentHealth += 20;
            //hpbar.SetHealth(playerhealth.currentHealth);
            Destroy(gameObject);
        }
    }
}
