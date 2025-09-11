using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variable
    [Header("---------Audio Source---------")]
    [SerializeField] public AudioSource bgSource;
    [SerializeField] public AudioSource SfxSource;
    //[SerializeField] public AudioSource dialogueSource;

    [Header("---------Audio Clip---------")]
    [Header("---------Audio Clip Game---------")]
    [SerializeField] public AudioClip bgClip;

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

    [Header("---------Audio Clip Lung---------")]
    [SerializeField] public AudioClip lungAttackClip;
    [SerializeField] public AudioClip lungHit;

    public static SoundManager instance;
    #endregion

    private void Start()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
        bgSource.loop = true;
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
