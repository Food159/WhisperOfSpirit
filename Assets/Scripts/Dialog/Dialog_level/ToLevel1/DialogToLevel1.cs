using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogToLevel1 : MonoBehaviour
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
    public GameObject deangBG1;
    public GameObject deangBG2;
    public GameObject Dara_Dialog_haha;
    public GameObject Dara_Dialog_exited;
    public GameObject Dara_Dialog_fine;
    public GameObject Dara_Dialog_angy;
    public GameObject Dara_Dialog_no1;
    public GameObject Dara_Dialog_thinking;

    public bool _isTyping = false;
    public bool _isChoiceFinished = false;
    public bool _showNextChoice = false;
    SoundManager soundmanager;
    #endregion

    private int index;
    private void Start()
    {
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
                Dara_Dialog_haha.SetActive(true);
            }
            if(index == 2)
            {
                Dara_Dialog_haha.SetActive(false);
                Dara_Dialog_exited.SetActive(true);
            }
            if (index == 3)
            {
                Dara_Dialog_exited.SetActive(false);
                Dara_Dialog_fine.SetActive(true);
            }
            if (index == 4)
            {
                Dara_Dialog_fine.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if (index == 5)
            {
                Dara_Dialog_haha.SetActive(false);
                Dara_Dialog_fine.SetActive(true);
            }
            if (index == 6)
            {
                Dara_Dialog_fine.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
            if (index == 7)
            {
                Dara_Dialog_haha.SetActive(false);
                oldBG.SetActive(false);
                deangBG1.SetActive(true);
            }
            if(index == 9)
            {
                deangBG1.SetActive(false);
                deangBG2.SetActive(true);
            }
            if(index == 10) 
            {
                deangBG2.SetActive(false);
                oldBG.SetActive(true);
                Dara_Dialog_angy.SetActive(true);
            }
            if(index == 12) 
            {
                Dara_Dialog_angy.SetActive(false);
                Dara_Dialog_no1.SetActive(true);
            }
            if (index == 14)
            {
                Dara_Dialog_no1.SetActive(false);
                Dara_Dialog_thinking.SetActive(true);
            }
            if(index == 17)
            {
                Dara_Dialog_thinking.SetActive(false);
                Dara_Dialog_no1.SetActive(true);
            }
            if(index == 25)
            {
                Dara_Dialog_no1.SetActive(false);
                Dara_Dialog_haha.SetActive(true);
            }
        }
        else
        {
            if(!isLoadScene)
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
