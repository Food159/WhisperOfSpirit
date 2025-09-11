using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ScrollLevel
{
    menu, boss
}
public class ScrollingTitle : MonoBehaviour
{
    public float speed = 5f;
    Vector3 startpos;
    float repeat;
    public ScrollLevel scrolllevel;

    private void Start()
    {
        startpos = transform.position;
        repeat = GetComponent<BoxCollider2D>().size.x / 2;
    }
    private void Update()
    {
        if(scrolllevel == ScrollLevel.menu) 
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (transform.position.x < startpos.x - repeat)
            {
                transform.position = startpos;
            }
        }
        else if(scrolllevel == ScrollLevel.boss) 
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (transform.position.x > startpos.x + repeat)
            {
                transform.position = startpos;
            }
        }
    }
}
