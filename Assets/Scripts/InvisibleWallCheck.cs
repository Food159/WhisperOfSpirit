using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit invisible wall!");
        }
    }
}
