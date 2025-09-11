using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteParalax : MonoBehaviour
{
    private float lenght, startpos;
    public GameObject cam;
    public float paralax;
    private void Start()
    {
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - paralax));
        float dist = (cam.transform.position.x * paralax);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //if (temp > startpos + lenght)
        //{
        //    startpos += lenght;
        //}
        if (temp > transform.position.x + lenght)
        {
            startpos += lenght;
        }
        else if (temp < transform.position.x - lenght)
        {
            startpos -= lenght;
        }
    }
}
