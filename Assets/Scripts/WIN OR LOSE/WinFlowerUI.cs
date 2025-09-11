using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinFlowerUI : MonoBehaviour
{
    [Header("Flower")]
    [SerializeField] Image FlowerImage;
    [SerializeField] Sprite noFlower;
    [SerializeField] Sprite oneFlower;
    [SerializeField] Sprite twoFlower;
    [SerializeField] Sprite threeFlower;

    [Space]
    [SerializeField]Track track;

    private int currentFlowerCount = 0;
    private bool isAnimating = false;
    private void Awake()
    {
        if(track == null)
        {
            track = FindAnyObjectByType<Track>();
        }
    }
    private void Update()
    {
        if (track == null)
            return;

        int completedCount = 0;
        if (track.trackCompleted) completedCount++;
        if (track.trackCompleted2) completedCount++;
        if (track.trackCompleted3) completedCount++;
        if(completedCount > currentFlowerCount && !isAnimating)
        {
            StartCoroutine(WaitFlower(completedCount));
        }

    }
    private IEnumerator WaitFlower(int targetCount)
    {
        isAnimating = true;
        while(currentFlowerCount < targetCount)
        {
            currentFlowerCount++;
            UpdateFlower();
            yield return new WaitForSeconds(1f);
        }
        isAnimating = false;
    }
    private void UpdateFlower()
    {
        switch (currentFlowerCount)
        {
            case 0:
                FlowerImage.sprite = noFlower;
                break;
            case 1:
                FlowerImage.sprite = oneFlower;
                break;
            case 2:
                FlowerImage.sprite = twoFlower;
                break;
            case 3:
                FlowerImage.sprite = threeFlower;
                break;
        }
    }
}
