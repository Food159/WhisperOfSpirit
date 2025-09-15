using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Subject
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool _isPlayerDead = false;
    public bool _hasLoseStart = false;
    public HealthBar healthbar;
    [SerializeField] GameObject Losepanel;

    [Header("Sprite")]
    public Image tawanimage;
    public Image daraimage;

    public Sprite TawanHappySprite;
    public Sprite TawanFineSprite;
    public Sprite TawanBadSprite;

    public Sprite DaraHappySprite;
    public Sprite DaraFineSprite;
    public Sprite DaraBadSprite;

    private void Start()
    {
        //currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
        if (_isPlayerDead == true)
            return;
        if (currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
    }
    private void Update()
    {
        if(currentHealth >= 100)
        {
            currentHealth = 100;
        }
        UpdateSprite();
        healthbar.SetHealth(currentHealth);
        if (_isPlayerDead && !_hasLoseStart)
        {
            StartCoroutine(LoseAfterDelay());
            _hasLoseStart = true;
        }
    }
    public void TakeDamage(int damage)
    {
        if (_isPlayerDead)
            return;
        SoundManager.instance.PlaySfx(SoundManager.instance.lungAttackClip);
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        UpdateSprite();
        if (currentHealth <= 0)
        {
            _isPlayerDead = true;
        }
    }
    IEnumerator LoseAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.instance.PlaySfx(SoundManager.instance.loseClip);
        Losepanel.SetActive(true);
    }
    void UpdateSprite()
    {
        if (currentHealth <= 20) 
        {
            tawanimage.sprite = TawanBadSprite;
            daraimage.sprite = DaraBadSprite;
        }
        else if (currentHealth <= 60)
        {
            tawanimage.sprite = TawanFineSprite;
            daraimage.sprite = DaraFineSprite;
        }
        else
        {
            tawanimage.sprite = TawanHappySprite;
            daraimage.sprite = DaraHappySprite;
        }
    }
}
