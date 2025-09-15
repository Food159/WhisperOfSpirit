using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Items : MonoBehaviour
{
    public LevelShop levelshop;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] InventoryUI inventory;
    [SerializeField] InventorySlot inventoryslot;
    //public TMP_Text timerText;
    public bool changeSprite = false;
    public bool damageIncrese = false;
    public bool reloadSpeed = false;
    private void Awake()
    {
        playerShooting = FindAnyObjectByType<PlayerShooting>();
        inventory = FindAnyObjectByType<InventoryUI>();
    }
    private void Update()
    {
        if(levelshop == LevelShop.Normal)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseItems(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseItems(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItems(2);
            }
        }
    }
    void UseItems(int index)
    {
        ItemsType type = inventory.GetItemType(index);
        switch (type) 
        {
            case ItemsType.mango:
                StartCoroutine(UseMango(index));
                inventory.RemoveItem(index);
                break;
            case ItemsType.rice:
                StartCoroutine(UseRice(index));
                inventory.RemoveItem(index);
                break;
            case ItemsType.green:
                StartCoroutine(UseGreen(index));
                inventory.RemoveItem(index);
                break;
            default:
                Debug.Log("NO ITEMS");
                break;
        }
    }
    IEnumerator UseMango(int index)
    {
        playerShooting.bulletSpeed = 10 / 0.5f; //10 / 0.5f = 20
        //playerShooting.waterReload = 1.25f * 0.5f; // 1.25 * 0.5 = 0.625
        damageIncrese = true;
        changeSprite = true;
        reloadSpeed = true;
        Debug.Log("UseMango");
        //yield return new WaitForSeconds(10f);
        yield return StartCoroutine(StartTimer(index, 10f));
        playerShooting.bulletSpeed = 10f;
        //playerShooting.waterReload = 1.25f;
        changeSprite = false;
        damageIncrese = false;
        reloadSpeed = false;
    }
    IEnumerator UseRice(int index)
    {
        playerShooting.bulletSpeed = 10 / 0.5f;
        //playerShooting.waterReload = 1.25f * 0.5f;
        reloadSpeed = true;
        Debug.Log("UseRice");
        //yield return new WaitForSeconds(5f);
        yield return StartCoroutine(StartTimer(index, 5f));
        playerShooting.bulletSpeed = 10f;
        //playerShooting.waterReload = 1.25f;
        reloadSpeed = false;
    }
    IEnumerator UseGreen(int index)
    {
        changeSprite = true;
        damageIncrese = true;
        Debug.Log("UseGreen");
        //yield return new WaitForSeconds(5f);
        yield return StartCoroutine(StartTimer(index, 5f));
        changeSprite = false;
        damageIncrese = false;
    }
    IEnumerator StartTimer(int index, float duration)
    {
        InventorySlot slot = inventory.GetSlot(index);
        if(slot == null || slot.timerText== null) 
        {
            yield break;
        }
        slot.timerText.gameObject.SetActive(true);
        float timeLeft = duration;
        while (timeLeft> 0)
        {
            slot.timerText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }
        slot.timerText.gameObject.SetActive(false);
    }
}
