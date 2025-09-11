using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public enum Language { English, Thai }
    public Language currentLanguage = Language.English;

    private Dictionary<string, string> localizedTexts = new Dictionary<string, string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            int saveLanguage = PlayerPrefs.GetInt("Language", 0);
            currentLanguage = (Language)saveLanguage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadName(NameData nameData)
    {
        foreach(var n in nameData.names) 
        {
            if(currentLanguage == Language.English)
            {
                localizedTexts[n.key] = n.englishName;
            }
            else if(currentLanguage == Language.Thai) 
            {
                localizedTexts[n.key] = n.thaiName;
            }
        }
    }
    public void Load(DialogData dialogData)
    {
        localizedTexts.Clear();
        foreach (var line in dialogData.lines)
        {
            if (currentLanguage == Language.English)
            {
                localizedTexts[line.key] = line.englishText;
            }
            else if (currentLanguage == Language.Thai)
            {
                localizedTexts[line.key] = line.thaiText;
            }
        }
    }

    public string GetText(string key)
    {
        if (localizedTexts.ContainsKey(key))
        {
            return localizedTexts[key];
        }
        return $"[MISSING: {key}]";
    }
    public void SetLanguage(Language language) 
    {
        currentLanguage = language;
        PlayerPrefs.SetInt("Language", (int)language);
        PlayerPrefs.Save();
    }
}
