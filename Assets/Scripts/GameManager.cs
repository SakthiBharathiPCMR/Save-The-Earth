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
    public Transition transitionScript;


    public Button playButton;
    public Button restartButton;
    public Button musicButton;
    public Text scoreText;
    public Text finalScoreText;
    private int score;

    private bool hasMusic = true;

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
        musicButton.onClick.AddListener(MusicToggle);
    }

    private void MusicToggle()
    {
        if (audioSource.isActiveAndEnabled)
            PlayInButtonClick();

        hasMusic = !hasMusic;
        audioSource.enabled = hasMusic;
        spawnManager.ToggleMusic(hasMusic);
        earthScript.ToggleAudio(hasMusic);
    }


    private void PlayInButtonClick()
    {
        audioSource.PlayOneShot(audioClips[0], 1f);
    }

    public void PlayInExplosion()
    {
        audioSource.PlayOneShot(audioClips[1], 0.5f);
    }

    public void PlayInDamage()
    {
        audioSource.PlayOneShot(audioClips[2], 0.5f);
    }

    private void StartGame()
    {
        //ScoreUI();

        PlayInButtonClick();
        StartCoroutine(StartDelay());

    }

    private IEnumerator StartDelay()
    {
        transitionScript.StartTransition();

        yield return new WaitForSeconds(.7f);
        score = 0;
        playButton.gameObject.SetActive(false);
        musicButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        isGameActive = true;
        scoreText.transform.parent.gameObject.SetActive(true);
        earthScript.gameObject.SetActive(true);
        spawnManager.StartGame();
        earthScript.StartGame();

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

        StartCoroutine(DelayGameOver());
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(.5f);

        restartButton.gameObject.SetActive(true);
        scoreText.transform.parent.gameObject.SetActive(false);
        earthScript.gameObject.SetActive(false);
    }


}
