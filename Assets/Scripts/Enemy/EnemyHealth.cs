using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool _isDead;
    public HealthBar healthbar;
    private void Start()
    {
        if (_isDead == true)
            return;
        if (currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        if (_isDead)
            return;

        currentHealth -= damage;
        SoundManager.instance.PlaySfx(SoundManager.instance.lungHit);
        healthbar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            _isDead = true;
        }
    }
}
