using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    TawanTitle tawanRun;

    private void Start()
    {
        tawanRun = FindObjectOfType<TawanTitle>();
    }
    public void OnButtonPlay()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        tawanRun.StartMoving();
    }
    public void OnButtonContinue()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        SceneData scenedata = SceneDataHandler.instance.LoadSceneData();
        if (scenedata != null)
        {
            SceneController.instance.LoadSceneName(scenedata.sceneName);
        }
        
    }
    public void OnButtonMenu()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        SceneManager.LoadScene("SceneMenu");
    }
    public void OnButtonGameOne()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        SceneManager.LoadScene("SceneGameOne");
    }
    public void OnButtonA()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
    }
    public void ExitButton()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        Application.Quit();
        print("Quit");
    }
}
