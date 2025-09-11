using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("CameraShake")]
    public Animator camAnim;
    private void Awake()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }
    public void CamShaking()
    {
        int rand = Random.Range(0, 4);
        if (rand == 0)
        {
            camAnim.SetTrigger("CamShake");
        }
        else if (rand == 1)
        {
            camAnim.SetTrigger("CamShake1");
        }
        else if (rand == 2)
        {
            camAnim.SetTrigger("CamShake2");
        }
        else if (rand == 3)
        {
            camAnim.SetTrigger("CamShake3");
        }
        else if (rand == 4)
        {
            camAnim.SetTrigger("CamShake4");
        }
    }
}
