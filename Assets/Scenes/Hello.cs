using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hello : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Hellow());
    }
    IEnumerator Hellow()
    {
        yield return new WaitForSeconds(3f);
        SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
