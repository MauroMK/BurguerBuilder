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

    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private int score;
    
    [Header("GameOver")]
    [SerializeField] private GameObject gameoverScreen;

    private int pointsToAdd = 20;
    private int pointsToLose = 10;
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


    public void AddPoints()
    {
        score += pointsToAdd;
        UpdateScoreText();
    }

    public void RemovePoints()
    {
        score -= pointsToLose;
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
        SceneManager.LoadScene(mainMenu);
    }
}
