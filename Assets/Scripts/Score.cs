using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int currentScore = 0, highestScore;
    public void AddScore(int score)
    {
        currentScore += score;
        AdjustHighScore(currentScore);
    }

    public void AdjustHighScore(int newScore)
    {
        if(newScore > highestScore)
        {
            PlayerPrefsManager.SetMasterScore(newScore);
        }
    }

    public void ResetHighestScore()
    {
        PlayerPrefsManager.SetMasterScore(0);
    }

    private void Start()
    {
        highestScore = PlayerPrefsManager.GetMasterScore();
    }
}
