using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
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
    public TextMeshProUGUI textContinue;
    public Image imageContinue;
    public bool _isChoice = false;

    [Header("Image")]
    public GameObject oldBG;
    public GameObject sakchaiBG;
    public GameObject newBG;
    public GameObject daraIMG1;
    public GameObject daraIMG2;
    public GameObject daraIMG3;

    [Header("Choice")]
    public GameObject Choice_1;
    public GameObject Choice_2;
    public GameObject Choice_3;

    public float textSpeed;
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
        _isChoice = false;

        soundmanager = SoundManager.instance;
        StartDialog();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == LocalizationManager.instance.GetText(lineKey[index]) && textNames.text == LocalizationManager.instance.GetText(nameKey[index]) && _isChoice == false) 
            {
                if (_showNextChoice)
                {
                    if (index == 15)
                    {
                        _isChoice = true;
                        Choice_2.SetActive(true);
                        _isChoiceFinished = true;
                        daraIMG2.SetActive(false);
                        daraIMG3.SetActive(true);
                    }
                    else if (index == 16)
                    {
                        _isChoice = true;
                        Choice_3.SetActive(true);
                        _isChoiceFinished = true;
                    }
                    _showNextChoice = false;
                }
                else
                {
                    NextLine();
                }
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
            //textContinue.gameObject.SetActive(true);
            imageContinue.gameObject.SetActive(true);
        }
        else
        {
            //textContinue.gameObject.SetActive(false);
            imageContinue.gameObject.SetActive(false);
        }
        if(index == 14 && !_isChoiceFinished)
        {
            _isChoice = true;
            Choice_1.SetActive(true);
            _isChoiceFinished = true;
            daraIMG1.SetActive(false);
            daraIMG2.SetActive(true);
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

        //soundmanager.dialogueSource.Stop();
        //soundmanager.PlayDialogue(soundmanager.dialogue);
        _isTyping = false;
        //soundmanager.dialogueSource.Stop();
    }
    public void NextLine()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.dialogClip);
        if(index < lineKey.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
            if(index == 2)
            {
                if (oldBG != null) oldBG.SetActive(false);
                if (sakchaiBG != null) sakchaiBG.SetActive(true);
            }
            if(index == 4)
            {
                //if(oldBG != null) oldBG.SetActive(false);
                if(sakchaiBG != null) sakchaiBG.SetActive(false);
                if (newBG!= null) newBG.SetActive(true);
            }
            if(index == 8 && daraIMG1 != null)
            {
                daraIMG1.SetActive(true);
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
