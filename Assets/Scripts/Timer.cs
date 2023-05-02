using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI timerText;

    private float timeRemaining = 300f;
    private bool timerIsRunning = false;
    
    void Update()
    {

        if (timerIsRunning)
        {
            Debug.Log("Timer Started");
            timerText.text = timeRemaining.ToString("N2");
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                gameManager.GameOver();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }
}
