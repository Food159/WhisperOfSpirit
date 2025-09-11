using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject info1;
    [SerializeField] GameObject info2;
    [SerializeField] GameObject info3;
    [SerializeField] GameObject confirm1;
    [SerializeField] GameObject confirm2;
    [SerializeField] GameObject confirm3;
    [SerializeField] GameObject notEnoughMoney;
    bool buying = false;

    [Space]
    [Header("Inventory")]
    [SerializeField] InventoryUI inventory;
    [SerializeField] Sprite mangoSprite;
    [SerializeField] Sprite riceSprite;
    [SerializeField] Sprite greenSprite;

    [Space]
    [SerializeField] ScoreManager scoremanager;
    [SerializeField] GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;
    private void Awake()
    {
        inventory = FindAnyObjectByType<InventoryUI>();
        scoremanager = FindAnyObjectByType<ScoreManager>();
    }
    private void Start()
    {
        eventSystem = EventSystem.current;
    }
    private void Update()
    {
        ShowInfo();
    }
    void ShowInfo()
    {
        if (buying || notEnoughMoney.activeSelf)
            return;
        info1.SetActive(false);
        info2.SetActive(false);
        info3.SetActive(false);

        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        foreach(RaycastResult result in results) 
        {
            if(result.gameObject.CompareTag("Mango"))
            {
                info1.SetActive(true);
            }
            else if (result.gameObject.CompareTag("Rice"))
            {
                info2.SetActive(true);
            }
            else if (result.gameObject.CompareTag("Green"))
            {
                info3.SetActive(true);
            }
        }
    }
    public void BuyMango()
    {
        if (buying || notEnoughMoney.activeSelf)
            return;
        confirm1.SetActive(true);
        buying = true;
    }
    public void BuyRice()
    {
        if (buying || notEnoughMoney.activeSelf)
            return;
        confirm2.SetActive(true);
        buying = true;
    }
    public void BuyGreen()
    {
        if (buying || notEnoughMoney.activeSelf)
            return;
        confirm3.SetActive(true);
        buying = true;
    }
    public void ConfirmBuyMango()
    {
        if(scoremanager.score >= 9)
        {
            scoremanager.score -= 9;
            Debug.Log("BuyMango");
            inventory.AddItems(mangoSprite, ItemsType.mango);
            confirm1.SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough money");
            notEnoughMoney.SetActive(true);
            confirm1.SetActive(false);
        }
        buying = false;
    }
    public void ConfirmBuyRice()
    {
        if (scoremanager.score >= 3)
        {
            scoremanager.score -= 3;
            Debug.Log("BuyRice");
            inventory.AddItems(riceSprite, ItemsType.rice);
            confirm2.SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough money");
            notEnoughMoney.SetActive(true);
            confirm2.SetActive(false);
        }
        buying = false;
    }
    public void ConfirmBuyGreen()
    {
        if (scoremanager.score >= 5)
        {
            scoremanager.score -= 5;
            Debug.Log("BuyGreen");
            inventory.AddItems(greenSprite, ItemsType.green);
            confirm3.SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough money");
            notEnoughMoney.SetActive(true);
            confirm3.SetActive(false);
        }
        buying = false;
    }
    public void NoBttn()
    {
        buying = false;
    }
}
