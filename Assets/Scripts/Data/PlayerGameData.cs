using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGameData
{
    public Vector2 playerPos;
    public int playerHp;
    public int playerScore;
    public bool playerDied;
    public List<ItemsType> inventoryItems = new List<ItemsType>();
}
