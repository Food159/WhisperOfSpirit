using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IpadTeen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject ipadPanel;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text roundText;

    [Space]
    [Header("Config")]
    [SerializeField] List<Sprite> colorSprites;
    [SerializeField] List<Button> cardButtons;
    public float timerPerRound = 20f;
    public int totalRound = 3;
    public bool success = false;

    private int currentRound = 1;
    private float timer;
    private List<Sprite> shuffleSprites;
    private Button firstCard, secondCard;
    private bool canSelect = true;
    private List<IPausable> pauseable = new List<IPausable>();
    private void Awake()
    {
        MonoBehaviour[] behaviours = FindObjectsOfType<MonoBehaviour>(true);
        foreach (var b in behaviours)
        {
            if (b is IPausable)
            {
                pauseable.Add((IPausable)b);
            }
        }
    }
    public void StartRound()
    {
        Time.timeScale = 0f;
        foreach (var p in pauseable)
        {
            p.Pause();
        }
        timer = timerPerRound;
        roundText.text = $"Round {currentRound}/{totalRound}";
        ShuffleCard();
        StartCoroutine(TimeCountDown());
    }
    private void ShuffleCard()
    {
        shuffleSprites = new List<Sprite>();
        foreach(var sprite in colorSprites)
        {
            shuffleSprites.Add(sprite);
            shuffleSprites.Add(sprite);
        }
        for(int i = 0; i < shuffleSprites.Count; i++)
        {
            Sprite temp = shuffleSprites[i];
            int random = Random.Range(i, shuffleSprites.Count);
            shuffleSprites[i] = shuffleSprites[random];
            shuffleSprites[random] = temp;
        }
        for (int i = 0; i < cardButtons.Count; i++)
        {
            int index = i;
            cardButtons[i].image.sprite = shuffleSprites[i];
            cardButtons[i].interactable = true;
            cardButtons[i].gameObject.SetActive(true);

            cardButtons[i].onClick.RemoveAllListeners();
            cardButtons[i].onClick.AddListener(() => OnClickCard(cardButtons[index]));
        }
    }
    private void OnClickCard(Button clickedButton)
    {
        if (!canSelect)
            return;
        if(firstCard == null)
        {
            firstCard = clickedButton;
            firstCard.interactable = false;
        }
        else if(secondCard == null && clickedButton != firstCard)
        {
            secondCard = clickedButton;
            secondCard.interactable = false;
            StartCoroutine(CheckMatch());
        }
    }
    IEnumerator CheckMatch()
    {
        canSelect = false;
        yield return new WaitForSecondsRealtime(0.5f);
        if(firstCard.image.sprite == secondCard.image.sprite) 
        {
            firstCard.gameObject.SetActive(false);
            secondCard.gameObject.SetActive(false);
            CheckRoundComplete();
        }
        else
        {
            firstCard.interactable = true;
            secondCard.interactable = true;
        }
        firstCard = null;
        secondCard = null;
        canSelect = true;
    }
    private void CheckRoundComplete()
    {
        bool allCleared = true;
        foreach(var bttn in cardButtons)
        {
            if(bttn.gameObject.activeSelf)
            {
                allCleared = false;
                break;
            }
        }
        if(allCleared)
        {
            Debug.Log("RoundComplete");
            NextRound();
        }
    }
    private IEnumerator TimeCountDown()
    {
        while(timer > 0)
        {
            timer -= Time.unscaledDeltaTime;
            timerText.text = Mathf.Ceil(timer).ToString();
            yield return null;
        }
        Debug.Log("Time Up");
        success = false;
        EndIpad();
        //NextRound();
    }
    private void NextRound()
    {
        StopAllCoroutines();
        if(currentRound < totalRound) 
        {
            currentRound++;
            StartRound();
        }
        else
        {
            success = true;
            EndIpad();
        }
    }
    private void EndIpad()
    {
        Time.timeScale = 1f;
        ipadPanel.SetActive(false);
        foreach (var p in pauseable)
        {
            p.Resume();
        }
    }
}
