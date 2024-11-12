using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;
    public bool isGameActive;
    public EarthScript earthScript;
    public SpawnManager spawnManager;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public Button playButton;
    public Button restartButton;
    public Button musicButton;
    public Text scoreText;
    public Text finalScoreText;
    private int score;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(StartGame);
    }


    private void StartGame()
    {
        //ScoreUI();
        score = 0;
        playButton.gameObject.SetActive(false);
        musicButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        StartCoroutine(StartDelay());

    }


    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.5f);

        isGameActive = true;
        spawnManager.StartGame();
        earthScript.StartGame();
        scoreText.transform.parent.gameObject.SetActive(true);
        earthScript.gameObject.SetActive(true);
    }

    public void UpdateScore()
    {
        score++;
        ScoreUI();
    }

    private void ScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    private void FinalScore()
    {
        finalScoreText.text = "Your Score: " + score;
    }


    public void GameOver()
    {
        isGameActive = false;
        FinalScore();
        restartButton.gameObject.SetActive(true);
        scoreText.transform.parent.gameObject.SetActive(false);
        earthScript.gameObject.SetActive(false);

    }


}
