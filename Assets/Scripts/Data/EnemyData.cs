using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public List<Vector2> enemyPos = new List<Vector2>();
    public List<int> enemyHp = new List<int>();
    public List<bool> enemyDied = new List<bool>();
}