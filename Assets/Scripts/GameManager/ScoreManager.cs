using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Track track;
    [SerializeField] TMP_Text scoreText;
    public int score;
    private bool scoreAdded;
    private bool scoreAdded2;
    private bool scoreAdded3;
    private void Awake()
    {
        track = FindAnyObjectByType<Track>();
    }
    private void Update()
    {
        if(track != null) 
        {
            if (track.trackCompleted && !scoreAdded)
            {
                score++;
                scoreAdded = true;
            }
            else if(track.trackCompleted2 && !scoreAdded2)
            {
                score++;
                scoreAdded2 = true;
            }
            else if (track.trackCompleted3 && !scoreAdded3)
            {
                score++;
                scoreAdded3 = true;
            }
        }
        scoreText.text = $"{score}";
    }
}
