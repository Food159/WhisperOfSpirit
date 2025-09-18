using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ice_Water : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject waterPrefabs;
    [SerializeField] GameObject icePrefabs;

    [Space]
    [Header("UI")]
    [SerializeField] TMP_Text timeCountText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject icePanel;
    [SerializeField] GameObject menu;

    [Space]
    [Header("Config")]
    [SerializeField] Transform spawnPoint;
    private float time = 30f;
    public bool success = false;
    private int currentScore;
    private int scoreRequire = 20;

    private GameObject currentObj;
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
    public void StartIceGame()
    {
        Time.timeScale = 0f;
        menu.SetActive(false);

        icePanel.SetActive(true);
        foreach(var p in pauseable)
        {
            p.Pause();
        }
        currentScore = 0;
        scoreText.text = $"Score : {currentScore}/{scoreRequire}";
        StartCoroutine(GameTimer());
        SpawnRandom();
    }
    private void SpawnRandom()
    {
        if(currentObj != null)
        {
            Destroy(currentObj);
        }
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            currentObj = Instantiate(icePrefabs, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
        else if(random == 1) 
        {
            currentObj = Instantiate(waterPrefabs, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
        currentObj.AddComponent<DragDrop>().icemanager = this;
    }
    public void CheckAnswer(string targetTag, GameObject obj)
    {
        if ((obj.CompareTag("Ice") && targetTag == "IceZone") || (obj.CompareTag("Water") && targetTag == "WaterZone"))
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.minigameCorrect);
            currentScore++;
            Destroy(obj);
            SpawnRandom();
        }
        else
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.minigameWrong);
            currentScore--;
            if(currentScore < 0) 
            {
                currentScore = 0;
            }
            obj.transform.position = spawnPoint.position;
        }
        scoreText.text = $"Score : {currentScore}/{scoreRequire}";
        if(currentScore >= scoreRequire)
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.minigameComplete);
            success = true;
            EndIce();
        }
    }
    private IEnumerator GameTimer()
    {
        float timer = time;
        while (timer > 0) 
        {
            timer -= Time.unscaledDeltaTime;
            timeCountText.text = Mathf.Ceil(timer).ToString();
            yield return null;
        }
        SoundManager.instance.PlaySfx(SoundManager.instance.minigameFail);
        timeCountText.text = "Time Up!!!";
        if(currentObj != null) 
        {
            Destroy(currentObj);
        }
        EndIce();
    }
    private void EndIce()
    {
        Time.timeScale = 1f;
        menu.SetActive(true);
        gameObject.SetActive(false);
        foreach (var p in pauseable)
        {
            p.Resume();
        }
    }
}
