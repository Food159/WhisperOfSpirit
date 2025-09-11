using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : Subject
{
    public int maxWater = 5;
    public int currentWater;
    public WaterBar waterbar;
    private void Start()
    {
        currentWater = maxWater;
        waterbar.SetMaxWater(maxWater);
    }
    public void shoot(int water)
    {
        currentWater -= water;
        waterbar.SetWater(currentWater);
    }
}
