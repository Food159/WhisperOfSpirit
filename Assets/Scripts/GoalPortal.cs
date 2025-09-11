using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPortal : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
        }
    }
}
