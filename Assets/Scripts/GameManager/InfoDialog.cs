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
        yield return new WaitForSeconds(10f);
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
            englishText = "Violence doesn’t make things fun… it only ruins the vibe, you know? Please don’t use violence when playing Songkran with others, okay?";
            thaiText = "ความรุนแรงไม่ทำให้สนุกหรอก มีแต่ทำให้ทุกคนเสียบรรยากาศนะ อย่าใช้ความรุนแรงในการเล่นสงกรานต์กับผู้อื่นเลยนะ";
        }
        else if (level == Levelll.levelTwo)
        {
            englishText = "Splashing cool water is fun enough! No need to hurt anyone, right~? So, don’t put ice in the water, okay? It’s really dangerous.";
            thaiText = "สาดน้ำเย็นๆ ก็พอแล้ว ไม่ต้องทำให้คนอื่นเจ็บตัวเนอะ~ อย่าใส่น้ำแข็งเลยน้า มันอันตรายจริงๆ นะ";
        }
        else if (level == Levelll.levelThree)
        {
            englishText = "Spraying colors into someone’s eyes is super dangerous... you might hurt them, you know~ Clear water is fun enough! Please don’t put colors in the water, okay?";
            thaiText = "ยิงสีใส่ตาแบบนี้อันตรายมากเลย.. ระวังทำให้คนอื่นเจ็บตัวน้า~ สาดน้ำใสๆ ก็สนุกแล้ว! อย่าเอาสีมาใส่น้ำเลยนะ";
        }
    }
}
