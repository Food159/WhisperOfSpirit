using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelEnter : MonoBehaviour
{
    [SerializeField] GameObject enterBttn;
    [SerializeField] GameObject enterPanel;
    public bool enterOpened = false;
    bool playerInNext = false;
    private void Update()
    {
        //if (enterOpened)
        //{
        //    Time.timeScale = 0f;
        //}
        //else if(!enterOpened)
        //{
        //    Time.timeScale = 1f;
        //}
        if (playerInNext && Input.GetKeyDown(KeyCode.E))
        {
            enterPanel.SetActive(true);
            enterOpened = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterBttn.SetActive(true);
            playerInNext = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterBttn.SetActive(false);
            playerInNext = false;
        }
    }
    public void YesEnter()
    {
        GameDataHandler.instance.SaveData();
        SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
        GameDataHandler.instance.LoadData();
    }
    public void CloseEnter()
    {
        enterPanel.SetActive(false);
        enterOpened = false;
    }
}
