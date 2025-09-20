using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    [SerializeField] GameObject fin;
    [SerializeField] GameObject homeBttn;
    private void Start()
    {
        StartCoroutine(FinStart());
    }
    IEnumerator FinStart()
    {
        yield return new WaitForSeconds(3f);
        fin.SetActive(true);
        yield return new WaitForSeconds(1f);
        homeBttn.SetActive(true);
    }
    public void HomeBttn()
    {
        SceneController.instance.LoadSceneName("SceneMenu");
    }
}
