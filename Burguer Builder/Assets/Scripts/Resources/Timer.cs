using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text timerTxt;

    [SerializeField] private Gradient gradient;
    [SerializeField] private Image image;

    [SerializeField] private float maxTime = 120f;
    private float gameTotalTimer = 120f;
    private bool timerOn;

    private void Start() 
    {
        timerOn = true;
        GetCurrentFill();
    }

    private void Update() 
    {
        if (timerOn)
        {
            if (maxTime > 0)
            {
                maxTime -= Time.deltaTime;
                UpdateTimer(maxTime);
            }
            else
            {
                Debug.Log("Time is up.");
                maxTime = 0;
                timerOn = false;
            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = maxTime / gameTotalTimer;  // Normalize the time between 0 and 1
        image.fillAmount = fillAmount;

        Color color = gradient.Evaluate(fillAmount); // Gets the color for the fill
        image.color = color; // Applies the color to the image
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        int seconds = Mathf.FloorToInt(currentTime); // Round to the nearest whole number
        timerTxt.text = seconds.ToString(); // Show only the seconds
    }
}
