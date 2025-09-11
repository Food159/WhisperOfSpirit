using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlate : MonoBehaviour
{
    [SerializeField] private WinCheck win;
    public LevelWinType levelwin;
    public bool plateWin = false;
    private void Awake()
    {
        if (win == null)
        {
            win = FindAnyObjectByType<WinCheck>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !win.wintestCheck && !plateWin)
        {
            win.Win();
            plateWin = true;
            Debug.Log("win");
        }
    }
}
