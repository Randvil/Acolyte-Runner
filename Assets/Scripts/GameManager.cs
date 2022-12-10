using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private int coinsRequired;

    [SerializeField]
    private TextMeshProUGUI timerUI;

    [SerializeField]
    private TextMeshProUGUI coinsUI;

    [SerializeField]
    private GameObject loseScreenUI;

    [SerializeField]
    private GameObject winScreenUI;

    [SerializeField]
    private TextMeshProUGUI loseScreenCoinsUI;

    [SerializeField]
    private TextMeshProUGUI winScreenCoinsUI;

    [SerializeField]
    private GameObject startScreenUI;

    public static GameManager instance;

    private int coins;

    private void Start()
    {
        instance = this;

        Time.timeScale = 0f;
    }

    private void Update()
    {
        float timeLeft = timeLimit - Time.timeSinceLevelLoad;
        if (timeLeft > 0f) timerUI.text = $"Time left: {timeLeft:N0}";
        else
        {
            timerUI.text = $"Time left: 0";
            StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame()
    {
        Time.timeScale = 0f;

        loseScreenUI.SetActive(true);
        loseScreenCoinsUI.text = $"{coins}/{coinsRequired}";

        yield return new WaitForSecondsRealtime(3f);

        loseScreenUI.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    private IEnumerator WinGame()
    {
        Time.timeScale = 0f;

        winScreenUI.SetActive(true);
        winScreenCoinsUI.text = $"{coins}/{coinsRequired}";

        yield return new WaitForSecondsRealtime(3f);

        winScreenUI.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void OnStart()
    {
        Time.timeScale = 1f;

        startScreenUI.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnCoinCollected()
    {
        coins += 1;
        coinsUI.text = $"Coins: {coins}";

        if (coins >= coinsRequired)
        {
            StartCoroutine(WinGame());
        }
    }

    public void OnCoinDecrease(int amount)
    {
        coins -= coins > amount ? amount : coins;
        coinsUI.text = $"Coins: {coins}";       
    }
}
