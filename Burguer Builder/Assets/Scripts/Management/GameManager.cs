using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer")]
    public float maxTime = 120f;

    [Header("Order")]
    [SerializeField] private GameObject correctOrderNumbers;
    private GamemodeTracker gamemode;

    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private int score;
    
    [Header("Game Over")]
    [SerializeField] private GameObject gameoverScreen;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    /* public int pointsToAdd = 20;
    public int pointsToLose = 10; */
    private string mainMenu = "MainMenu";

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start() 
    {
        gamemode = FindObjectOfType<GamemodeTracker>();

        if (gamemode.randomOrderMode == true)
        {
            correctOrderNumbers.SetActive(false);
        }
        else
        {
            correctOrderNumbers.SetActive(true);
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }    
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreText();
    }

    public void RemovePoints(int pointsToRemove)
    {
        score -= pointsToRemove;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        finalScoreText.text = score.ToString();
    }

    public void ShowEndgameScreen()
    {
        gameoverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    internal void AddPoints(object value)
    {
        throw new NotImplementedException();
    }
}
