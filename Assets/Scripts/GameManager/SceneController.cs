using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionanim;
    [SerializeField] GameObject scentransition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadSceneIndex(int sceneIndex)
    {
        if(sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadLevelIndex(sceneIndex));
        }
        else
        {
            Debug.Log("Error no index");
        }
    }
    public void LoadSceneName(string sceneName)
    {
        StartCoroutine(LoadLevelName(sceneName));
    }
    IEnumerator LoadLevelName(string sceneName)
    {
        scentransition.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName); // ใช้ SceneController.instance.LoadSceneByName("sceneName"); เพื่อ loadscene ที่ต้องการ
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        scentransition.SetActive(false);
    }
    IEnumerator LoadLevelIndex(int sceneIndex)
    {
        scentransition.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        scentransition.SetActive(false);
    }
}
