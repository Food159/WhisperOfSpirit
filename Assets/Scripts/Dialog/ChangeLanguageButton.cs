using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLanguageButton : MonoBehaviour
{
    public TMP_Text buttonTextEng;
    public TMP_Text buttonTextThai;
    public TMP_Text currentLanguage;
    [Header("Localized Text")]
    private string englishText = "English";
    private string englishThaiText = "Thai";

    private string thaiText = "ภาษาไทย";
    private string thaiEngText = "ภาษาอังกฤษ";
    private void Start()
    {
        if(LocalizationManager.instance.currentLanguage == LocalizationManager.Language.English)
        {
            buttonTextEng.text = englishText;
            buttonTextThai.text = englishThaiText;
            currentLanguage.text = $"Subtitle : {englishText}";
        }
        else if(LocalizationManager.instance.currentLanguage == LocalizationManager.Language.Thai)
        {
            buttonTextEng.text = thaiEngText;
            buttonTextThai.text = thaiText;
            currentLanguage.text = $"Subtitle : {thaiText}";
        }
    }
    public void SetEnglish()
    {
        LocalizationManager.instance.SetLanguage(LocalizationManager.Language.English);
        buttonTextEng.text = englishText;
        buttonTextThai.text = englishThaiText;
        currentLanguage.text = $"Subtitle : {englishText}";
    }

    public void SetThai()
    {
        LocalizationManager.instance.SetLanguage(LocalizationManager.Language.Thai);
        buttonTextEng.text = thaiEngText;
        buttonTextThai.text = thaiText;
        currentLanguage.text = $"Subtitle : {thaiText}";
    }
}
