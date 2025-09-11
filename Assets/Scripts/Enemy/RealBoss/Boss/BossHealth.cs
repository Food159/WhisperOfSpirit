using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    private int maxHealth = 1600;
    public int currentHealth;
    public bool isDead;
    public HealthBar healthBar;
    [SerializeField] GameObject hpRegenPrefabs;
    private float nextSpawnThreshold;
    public BossPhase phase;
    bool change = false;
    private void Start()
    {
        if (isDead)
            return;
        if(currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        nextSpawnThreshold = maxHealth * 0.8f;
    }
    public void TakeDamage(int damage)
    {
        if (isDead) 
            return;
        currentHealth -= damage;
        SoundManager.instance.PlaySfx(SoundManager.instance.lungHit);
        healthBar.SetHealth(currentHealth);
        if(phase == BossPhase.phase1)
        {
            if (currentHealth <= maxHealth * 0.35f && !change)
            {
                change = true;
                GameDataHandler.instance.SaveDataBoss();
                SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
                GameDataHandler.instance.LoadDataBoss();
            }
        }
        if(currentHealth <= nextSpawnThreshold && !isDead)
        {
            SpawnRegenItem();
            nextSpawnThreshold -= maxHealth * 0.2f;
        }
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }
    private void SpawnRegenItem()
    {
        float randomX = Random.Range(-13f, 4.7f);
        Vector2 spawnPos = new Vector2(randomX, -2.3f);
        Instantiate(hpRegenPrefabs, spawnPos, Quaternion.identity);
    }
}
