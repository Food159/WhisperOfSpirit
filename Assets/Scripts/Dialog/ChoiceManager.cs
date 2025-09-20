using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public Dialog dialog;
    bool isLoadScene = false;
    [SerializeField] TMP_Text choiceYes1;
    [SerializeField] TMP_Text choiceNo1;
    private string choiceYes1TextEng = "Alright!";
    private string choiceYes1TextTh = "ได้!";
    private string choiceNo1TextEng = "Nah, I'm gonna go splash some water!";
    private string choiceNo1TextTh = "ไม่เอาอ่ะ ไม่ว่าง จะไปเล่นน้ำ";

    [SerializeField] TMP_Text choiceYes2;
    [SerializeField] TMP_Text choiceNo2;
    private string choiceYes2TextEng = "Alright then, I'm with you.";
    private string choiceYes2TextTh = "ไปด้วยก็ได้";
    private string choiceNo2TextEng = "Yeah, I wanna go see Moo Deng!";
    private string choiceNo2TextTh = "ก็ไม่ว่างอ่ะ อยากไปดูหมูเด้ง";

    [SerializeField] TMP_Text choiceYes3;
    [SerializeField] TMP_Text choiceNo3;
    private string choiceYes3TextEng = "*sigh* Okay.";
    private string choiceYes3TextTh = "เฮ้อ โอเค";
    private string choiceNo3TextEng = "Nooope! Gotta go now, See yaaa!";
    private string choiceNo3TextTh = "ไม่เอาอะ ไปเล่นน้ำละ บายยย";


    private void Awake()
    {
        if (dialog == null)
        {
            dialog = FindObjectOfType<Dialog>();
        }
    }
    private void Start()
    {
        if (LocalizationManager.instance.currentLanguage == LocalizationManager.Language.English)
        {
            choiceYes1.text = choiceYes1TextEng;
            choiceNo1.text = choiceNo1TextEng;
            choiceYes2.text = choiceYes2TextEng;
            choiceNo2.text = choiceNo2TextEng;
            choiceYes3.text = choiceYes3TextEng;
            choiceNo3.text = choiceNo3TextEng;
        }
        else if (LocalizationManager.instance.currentLanguage == LocalizationManager.Language.Thai)
        {
            choiceYes1.text = choiceYes1TextTh;
            choiceNo1.text = choiceNo1TextTh;
            choiceYes2.text = choiceYes2TextTh;
            choiceNo2.text = choiceNo2TextTh;
            choiceYes3.text = choiceYes3TextTh;
            choiceNo3.text = choiceNo3TextTh;
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
