using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InfoDialog : MonoBehaviour
{
    bool infoOpened = false;
    [SerializeField] GameObject infoD;
    public Levelll level;

    [Header("Localized Text")]
    [SerializeField] TMP_Text textInfotext;

    private string englishText = "English";
    private string thaiText = "ภาษาไทย";
    private void Start()
    {
        SceneEIEI();
        LanguageChangeEIEI();
        if (LocalizationManager.instance.currentLanguage == LocalizationManager.Language.English)
        {
            textInfotext.text = englishText;
        }
        else if(LocalizationManager.instance.currentLanguage == LocalizationManager.Language.Thai)
        {
            textInfotext.text = thaiText;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !infoOpened)
        {
            infoD.SetActive(true);
            infoOpened = true;
            StartCoroutine(InfoClose());
        }
    }
    IEnumerator InfoClose()
    {
        yield return new WaitForSeconds(6f);
        infoD.SetActive(false);
    }
    void SceneEIEI()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SceneMenu" || sceneName == "SceneDialogueOne" || sceneName == "SceneGameOne")
        {
            level = Levelll.levelTutorial;
        }
        else if (sceneName == "SceneGameTwo")
        {
            level = Levelll.levelOne;
        }
        else if (sceneName == "SceneGameThree")
        {
            level = Levelll.levelTwo;
        }
        else if (sceneName == "SceneGameFour")
        {
            level = Levelll.levelThree;
        }
        else if (sceneName == "SceneGameShop" || sceneName == "SceneGameBoss" || sceneName == "Hello" || sceneName == "SceneGameBossPhase2")
        {
            level = Levelll.levelBoss;
        }
    }
    void LanguageChangeEIEI()
    {
        if (level == Levelll.levelOne)
        {
            englishText = "One";
            thaiText = "หนึ่ง";
        }
        else if (level == Levelll.levelTwo)
        {
            englishText = "Two";
            thaiText = "สอง";
        }
        else if (level == Levelll.levelThree)
        {
            englishText = "Three";
            thaiText = "สาม";
        }
    }
}
