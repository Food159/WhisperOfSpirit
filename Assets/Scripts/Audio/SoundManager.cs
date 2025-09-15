using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Levelll
{
    levelTutorial, levelOne, levelTwo, levelThree, levelBoss
}
public class SoundManager : MonoBehaviour
{
    #region Variable
    public Levelll level;
    [Header("---------Audio Source---------")]
    [SerializeField] public AudioSource bgSource;
    [SerializeField] public AudioSource SfxSource;
    //[SerializeField] public AudioSource dialogueSource;

    [Header("---------Audio Clip---------")]
    [Header("---------Audio Clip Game---------")]
    [SerializeField] public AudioClip bgClip;
    [SerializeField] public AudioClip bgFirstClip;
    [SerializeField] public AudioClip bgSecondClip;
    [SerializeField] public AudioClip bgThirdClip;
    [SerializeField] public AudioClip bgBossClip;

    [SerializeField] public AudioClip choiceSelectedClip;
    [SerializeField] public AudioClip dialogClip;
    [SerializeField] public AudioClip loseClip;
    [SerializeField] public AudioClip winClip;
    [SerializeField] public AudioClip enemyPurifyClip;

    [Header("---------Audio Clip Tawan---------")]
    [SerializeField] public AudioClip tawanWalkClip;
    [SerializeField] public AudioClip tawanRunClip;
    [SerializeField] public AudioClip tawanJumpClip;
    [SerializeField] public AudioClip tawanShootWaterClip;

    [Header("---------Audio Clip Enemy---------")]
    [SerializeField] public AudioClip lungAttackClip;
    [SerializeField] public AudioClip lungHit;
    [SerializeField] public AudioClip blind;

    [Header("---------Audio Clip Items---------")]
    [SerializeField] public AudioClip itemsPick;

    [Header("---------Audio Clip Minigame---------")]
    [SerializeField] public AudioClip minigameCorrect;
    [SerializeField] public AudioClip minigameWrong;
    [SerializeField] public AudioClip minigameComplete;
    [SerializeField] public AudioClip minigameFail;

    public static SoundManager instance;
    #endregion

    private void Start()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
        bgSource.loop = true;
    }
    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SceneMenu" || sceneName == "SceneDialogueOne" || sceneName == "SceneGameOne")
        {
            level = Levelll.levelTutorial;
        }
        else if(sceneName == "SceneGameTwo")
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
        AudioClip targetClip = null;
        switch(level)
        {
            case Levelll.levelTutorial:
                targetClip = bgClip;
                break;
            case Levelll.levelOne:
                targetClip = bgFirstClip;
                break;
            case Levelll.levelTwo:
                targetClip = bgSecondClip;
                break;
            case Levelll.levelThree:
                targetClip = bgThirdClip;
                break;
            case Levelll.levelBoss:
                targetClip = bgBossClip;
                break;
        }
        if(bgSource.clip != targetClip)
        {
            bgSource.clip = targetClip;
            bgSource.loop = true;
            bgSource.Play();
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySfx(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
}
