using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnter : MonoBehaviour
{
    [SerializeField] GameObject enterBttn;
    [SerializeField] GameObject shopPanel;
    bool shopOpened = false;
    bool playerInshop = false;
    private void Update()
    {
        if(shopOpened)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if(playerInshop && Input.GetKeyDown(KeyCode.E))
        {
            shopPanel.SetActive(true);
            shopOpened = true;
        }
        if(shopOpened && Input.GetKeyDown(KeyCode.Escape))
        {
            shopPanel.SetActive(false);
            shopOpened = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enterBttn.SetActive(true);
            playerInshop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterBttn.SetActive(false);
            playerInshop = false;
        }
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        shopOpened = false;
    }
}
