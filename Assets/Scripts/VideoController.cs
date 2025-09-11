using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoController : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject closeButton;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<string> videoFiles;
    [SerializeField] Image tutorialImage;
    [SerializeField] Sprite controllSprite;
    [SerializeField] Sprite combatSprite;

    [SerializeField] Image indicator1;
    [SerializeField] Image indicator2;
    [SerializeField] Image indicator3;
    [SerializeField] Image indicator4;
    [SerializeField] Image indicator5;
    [SerializeField] Sprite indicatorSelected;
    [SerializeField] Sprite indicatorNotSelected;
    private int videoIndex = 0;
    
    [SerializeField] TMP_Text textTutorial;
    public string[] text;
    private void Start()
    {
        StartCoroutine(WaitAfterTransition());
    }
    public void OnButtonNext()
    {
        videoIndex = (videoIndex + 1) % videoFiles.Count;
        PlayCurrentVideo(videoIndex);
    }
    public void OnButtonPrevious() 
    {
        videoIndex = (videoIndex - 1 + videoFiles.Count) % videoFiles.Count;
        PlayCurrentVideo(videoIndex);
    }
    public void OnButtonClose()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
    void PlayCurrentVideo(int index)
    {
        if (videoPlayer != null && videoFiles.Count > 0)
        {
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, videoFiles[index]);
            videoPlayer.url = path;
            videoPlayer.Play();
            UpdateTutorial(index);
        }
    }
    void UpdateTutorial(int index)
    {
        if(tutorialImage != null)
        {
            if(index >= 3)
            {
                tutorialImage.sprite = combatSprite;
            }
            else
            {
                tutorialImage.sprite = controllSprite;
            }
        }
        if(index == 0)
        {
            textTutorial.text = text[0];
            indicator1.sprite = indicatorSelected;
        }
        else
        {
            indicator1.sprite = indicatorNotSelected;
        }
        if (index == 1)
        {
            textTutorial.text = text[1];
            indicator2.sprite = indicatorSelected;
        }
        else
        {
            indicator2.sprite = indicatorNotSelected;
        }
        if (index == 2)
        {
            textTutorial.text = text[2];
            indicator3.sprite = indicatorSelected;
        }
        else
        {
            indicator3.sprite = indicatorNotSelected;
        }
        if (index == 3)
        {
            textTutorial.text = text[3];
            indicator4.sprite = indicatorSelected;
        }
        else
        {
            indicator4.sprite = indicatorNotSelected;
        }
        if (index == 4)
        {
            textTutorial.text = text[4];
            indicator5.sprite = indicatorSelected;
        }
        else
        {
            indicator5.sprite = indicatorNotSelected;
        }
        if (index >= 4)
        {
            closeButton.SetActive(true);
        }
    }
    IEnumerator WaitAfterTransition()
    {
        yield return new WaitForSeconds(1);
        tutorialPanel.SetActive(true);
        Time.timeScale = 0;
        PlayCurrentVideo(videoIndex);
    }
}
