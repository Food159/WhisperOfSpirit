using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    #region Variable
    [Header("Variable")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textNames;
    public TextMeshProUGUI textContinue;
    public Image imageContinue;
    public bool _isChoice = false;

    [Header("Image")]
    public GameObject oldBG;
    public GameObject newBG;
    public GameObject daraIMG1;
    public GameObject daraIMG2;
    public GameObject daraIMG3;

    [Header("Choice")]
    public GameObject Choice_1;
    public GameObject Choice_2;
    public GameObject Choice_3;

    [TextArea(3,10)]
    public string[] lines;
    public string[] names;
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
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        _isChoice = false;

        soundmanager = SoundManager.instance;
        StartDialog();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index] && textNames.text == names[index] && _isChoice == false) 
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
                textComponent.text = lines[index];
                textNames.text = names[index];
                _isTyping = false;
            }
        }
        if(textComponent.text == lines[index])
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
        textNames.text = names[index];

        _isTyping = true;

        //soundmanager.dialogueSource.Stop();
        //soundmanager.PlayDialogue(soundmanager.dialogue);

        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);
        for (int i = 0; i < maxLength; i++) 
        {
            //if(i < names[index].Length)
            //{
            //    textNames.text += names[index][i];
            //}
            if(i < lines[index].Length) 
            {
                textComponent.text += lines[index][i];
            }
            yield return new WaitForSeconds(textSpeed);
        }
        _isTyping = false;
        //soundmanager.dialogueSource.Stop();
    }
    public void NextLine()
    {
        SoundManager.instance.PlaySfx(SoundManager.instance.dialogClip);
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdateName();
            StartCoroutine(TypeLine());
            if(index == 4)
            {
                if(oldBG != null) oldBG.SetActive(false);
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
        if(index < names.Length) 
        {
            textNames.text = names[index];
        }
        else 
        {
            textNames.text = string.Empty;
        }
    }
}
