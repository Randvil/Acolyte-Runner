using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum eTimerType
    {
        Simple,
        Countdown
    }

    private enum eWinCondition
    {
        Infinite,
        Coins
    }

    [SerializeField]
    private eTimerType timerType = eTimerType.Simple;

    [SerializeField]
    private eWinCondition winCondition = eWinCondition.Infinite;

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

    public int coins = 0;
    private Coroutine audioClipCoroutine;

    public AudioSource audioSource;

    private void Start()
    {
        instance = this;

        Time.timeScale = 0f;

        audioSource = GetComponent<AudioSource>();

        audioSource.enabled = StaticSettings.soundOn;
    }

    private void Update()
    {
        if (timerType == eTimerType.Simple)
        {
            SimpleTimer();
        }
        else
        {
            Countdown();
        }
    }

    private void SimpleTimer()
    {
        timerUI.text = $"Time: {Time.timeSinceLevelLoad:N0}";
    }

    private void Countdown()
    {
        float timeLeft = timeLimit - Time.timeSinceLevelLoad;
        if (timeLeft > 0f) timerUI.text = $"Time: {timeLeft:N0}";
        else
        {
            timerUI.text = $"Time: 0";
            LoseGame();
        }
    }

    public void LoseGame()
    {
        StartCoroutine(LoseGameCoroutine());
    }

    private IEnumerator LoseGameCoroutine()
    {
        Time.timeScale = 0f;

        loseScreenUI.SetActive(true);
        if (winCondition == eWinCondition.Coins) loseScreenCoinsUI.text = $"{coins}/{coinsRequired}";
        else loseScreenCoinsUI.text = $"{coins}";

        yield return new WaitForSecondsRealtime(3f);

        loseScreenUI.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(0);
    }

    public void OnCoinCollected()
    {
        coins += 1;
        coinsUI.text = $"{coins}";

        if (coins >= coinsRequired && winCondition == eWinCondition.Coins)
        {
            StartCoroutine(WinGame());
        }
    }

    public void OnCoinDecrease(int amount)
    {
        coins -= coins > amount ? amount : coins;
        coinsUI.text = $"{coins}";       
    }

    public void PlayAudioClip(AudioClip clip, bool forceSound = false)
    {
        if (audioClipCoroutine == null || forceSound) audioClipCoroutine = StartCoroutine(AudioClipCoroutine(clip));
    }

    public IEnumerator AudioClipCoroutine(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        audioClipCoroutine = null;
    }
}
