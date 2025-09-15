using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeenBullet : MonoBehaviour
{
    public float lifeTime = 5f;
    private float timer;
    [SerializeField] private GameObject[] colourTeen;
    [SerializeField] private Canvas uiCanvas;
    private CameraShake camerashake;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Awake()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
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
            Debug.Log("Bullet Enemy Teen Collided with: " + collision.gameObject.name);
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
                camerashake.CamShaking();
            }
            if(colourTeen.Length > 0) 
            {
                SoundManager.instance.PlaySfx(SoundManager.instance.blind);
                int randomeffect = Random.Range(0, colourTeen.Length);
                GameObject effect = Instantiate(colourTeen[randomeffect], uiCanvas.transform);
                effect.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
