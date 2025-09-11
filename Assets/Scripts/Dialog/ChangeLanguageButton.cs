using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguageButton : MonoBehaviour
{
    public void SetEnglish()
    {
        LocalizationManager.instance.SetLanguage(LocalizationManager.Language.English);
    }

    public void SetThai()
    {
        LocalizationManager.instance.SetLanguage(LocalizationManager.Language.Thai);
    }
}
