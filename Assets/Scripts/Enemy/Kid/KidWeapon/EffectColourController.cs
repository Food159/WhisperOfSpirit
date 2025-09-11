using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColourController : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    private void OnEnable()
    {
        StartCoroutine(DestroyAfterSeconds());
    }
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
