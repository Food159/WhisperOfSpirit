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

    //[Header("Image")]
    //public GameObject oldBG;
    //public GameObject newBG;
    //public GameObject daraIMG1;
    //public GameObject daraIMG2;
    //public GameObject daraIMG3;

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
