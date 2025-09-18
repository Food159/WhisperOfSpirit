using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    public Dialog dialog;
    bool isLoadScene = false;
    private void Awake()
    {
        if (dialog == null)
        {
            dialog = FindObjectOfType<Dialog>();
        }
    }
    public void OnButtonNo()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
        if (dialog.Choice_1.activeSelf)
        {
            dialog.Choice_1.SetActive(false);
            dialog._isChoice = false;
            dialog._isChoiceFinished = false;

            dialog.NextLine();
            dialog._showNextChoice = true;
        }
        else if(dialog.Choice_2.activeSelf) 
        {
            dialog.Choice_2.SetActive(false);
            dialog._isChoice = false;
            dialog._isChoiceFinished = false;

            dialog.NextLine();
            dialog._showNextChoice = true;
        }
        else
        {
            dialog.Choice_3.SetActive(false);
            SceneController.instance.LoadSceneName("SceneMenu");
        }
    }
    public void OnButtonYesFirst() 
    {
        if(!isLoadScene)
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
            GameDataHandler.instance.ClearData();
            SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
            isLoadScene = true;
        }

    }
    public void OnButtonYesSecond()
    {
        if(!isLoadScene)
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.choiceSelectedClip);
            SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
            isLoadScene = true;
        }
    }
}
