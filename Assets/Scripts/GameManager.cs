using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Timer timer;

    public bool isGameActive;
    private float money;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + money.ToString("N2");
    }

    public void AddPay(float pay)
    {
        money += pay;
    }

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        timer.StartTimer();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
