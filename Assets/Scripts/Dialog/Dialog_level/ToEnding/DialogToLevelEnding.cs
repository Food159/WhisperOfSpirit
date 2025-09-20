using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogToLevelEnding : MonoBehaviour
{
    #region Variable
    [Header("Localization")]
    public DialogData dialogData;
    public NameData nameData;
    public string[] lineKey;
    public string[] nameKey;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textNames;

    [Header("Variable")]
    public Image imageContinue;
    bool isLoadScene = false;

    [Header("Image")]
    public GameObject oldBG;
    public GameObject S06_gamescene;
    public GameObject S07_Tawanhome;
    public GameObject S07_DarasLore01;
    public GameObject S07_DarasLore02;
    public GameObject S07_DarasLore03;
    public GameObject Op_buildy;
    public GameObject FinalEndScene;
    public GameObject Deang_Dialog_angy;
    public GameObject Dara_Dialog_angy;
    public GameObject Dara_Dialog_thinking;
    public GameObject Dara_Dialog_haha;
    public GameObject Dara_Dialog_no1;
    public GameObject Dara_Dialog_fine;
    public GameObject Dara_Dialog_soft;

    public bool _isTyping = false;
    public bool _isChoiceFinished = false;
    public bool _showNextChoice = false;
    SoundManager soundmanager;
    #endregion

    private int index;
    private void Start()
    {
        Deang_Dialog_angy.SetActive(true);
        SoundManager.instance.PlaySfx(SoundManager.instance.dialogClip);
        LocalizationManager.instance.Load(dialogData);
        LocalizationManager.instance.LoadName(nameData);

        soundmanager = SoundManager.instance;
        StartDialog();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == LocalizationManager.instance.GetText(lineKey[index]) && textNames.text == LocalizationManager.instance.GetText(nameKey[index]))
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = LocalizationManager.instance.GetText(lineKey[index]);
                textNames.text = LocalizationManager.instance.GetText(nameKey[index]);
                _isTyping = false;
            }
        }
        if (textComponent.text == LocalizationManager.instance.GetText(lineKey[index]))
        {
            imageContinue.gameObject.SetActive(true);
        }
        else
        {
            imageContinue.gameObject.SetActive(false);
        }
    }

    void StartDialog()
    {
        index = 0;
        UpdateName();
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        textNames.text = LocalizationManager.instance.GetText(nameKey[index]);
        string line = LocalizationManager.instance.GetText(lineKey[index]);
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        _isTyping = true;
        _isTyping = false;
    }
    public void NextLine()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.dialogClip);
        if (index < lineKey.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
            //if (index == 4)
            //{
            //    if (oldBG != null) oldBG.SetActive(false);
            //    if (newBG != null) newBG.SetActive(true);
            //}
            //if (index == 8 && daraIMG1 != null)
            //{
            //    daraIMG1.SetActive(true);
            //}
            if(index == 1)
            {
                Deang_Dialog_angy.SetActive(false);
                Dara_Dialog_angy.SetActive(true);
            }
            if(index == 3)
            {
                Dara_Dialog_angy.SetActive(false);
                Deang_Dialog_angy.SetActive(true);
            }
            if(index == 4)
            {
                Deang_Dialog_angy.SetActive(false);
                Dara_Dialog_angy.SetActive(true);
            }
            if (index == 6)
            {
                Dara_Dialog_angy.SetActive(false);
                Deang_Dialog_angy.SetActive(true);
            }
            if (index == 7)
            {
                Deang_Dialog_angy.SetActive(false);
                Dara_Dialog_angy.SetActive(true);
            }
            if(index == 8) 
            {
                Dara_Dialog_angy.SetActive(false);
                Dara_Dialog_thinking.SetActive(true);
            }
            if(index == 10)
            {
                Dara_Dialog_thinking.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if (index == 11) 
            {
                Dara_Dialog_haha.SetActive(false);
                Deang_Dialog_angy.SetActive(true);
            }
            if(index == 16)
            {
                Deang_Dialog_angy.SetActive(false);
                Dara_Dialog_no1.SetActive(true);
            }
            if (index == 19) 
            {
                Dara_Dialog_no1.SetActive(false);
                Dara_Dialog_fine.SetActive(true);
            }
            if(index == 21)
            {
                oldBG.SetActive(false);
                S06_gamescene.SetActive(true);
                Dara_Dialog_fine.SetActive(false);
                Dara_Dialog_soft.SetActive(true);
            }
            if(index == 25)
            {
                Dara_Dialog_soft.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if(index == 26) 
            {
                S06_gamescene.SetActive(false);
                S07_DarasLore01.SetActive(true);
                Dara_Dialog_haha.SetActive(false);
            }
            if(index == 27) 
            {
                S07_DarasLore01.SetActive(false);
                S07_DarasLore02.SetActive(true);
            }
            if(index == 29) 
            {
                S07_DarasLore02.SetActive(false);
                S07_DarasLore03.SetActive(true);
            }
            if(index == 30)
            {
                S07_DarasLore03.SetActive(false);
                S06_gamescene.SetActive(true);
                Dara_Dialog_soft.SetActive(true);
            }
            if(index == 31)
            {
                Dara_Dialog_soft.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if(index == 32)
            {
                Dara_Dialog_haha.SetActive(false);
                Dara_Dialog_soft.SetActive(true);
            }
            if(index == 36)
            {
                Dara_Dialog_soft.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if(index == 37)
            {
                Dara_Dialog_haha.SetActive(false);
                Dara_Dialog_soft.SetActive(true);
            }
            if(index == 40)
            {
                Dara_Dialog_soft.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if(index == 42)
            {
                Dara_Dialog_haha.SetActive(false);
                Dara_Dialog_soft.SetActive(true);
            }
            if(index == 44)
            {
                S06_gamescene.SetActive(false);
                S07_Tawanhome.SetActive(true);
                Dara_Dialog_soft.SetActive(false);
            }
            if(index == 46)
            {
                S07_Tawanhome.SetActive(false);
                Op_buildy.SetActive(true);
            }
            if(index == 47)
            {
                Op_buildy.SetActive(false);
                FinalEndScene.SetActive(true);
            }
        }
        else
        {
            if (!isLoadScene)
            {
                SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
                isLoadScene = true;
            }
        }
    }
    void UpdateName()
    {
        if (index < nameKey.Length)
        {
            textNames.text = LocalizationManager.instance.GetText(nameKey[index]);
        }
    }
}
