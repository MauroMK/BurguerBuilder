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

    private float maxTimeCatcher;
    private float gameTotalTimer = 120f;
    private bool timerOn;

    private void Start() 
    {
        maxTimeCatcher = GameManager.instance.maxTime;
        timerOn = true;
        GetCurrentFill();
    }

    private void Update() 
    {
        if (timerOn)
        {
            if (maxTimeCatcher > 0)
            {
                maxTimeCatcher -= Time.deltaTime;
                UpdateTimer(maxTimeCatcher);
            }
            else
            {
                GameManager.instance.ShowEndgameScreen();
                AudioManager.instance.PlaySound("FinalBell");
                maxTimeCatcher = 0;
                timerOn = false;
            }
        }

        GetCurrentFill();
    }


    private void UpdateTimer(float currentTime)
    {
        int seconds = Mathf.Max(Mathf.FloorToInt(currentTime), 0); // Round to the nearest whole number, clamped to minimum 0 so he don't reach -1
        timerTxt.text = seconds.ToString(); // Show only the seconds
    }

    void GetCurrentFill()
    {
        float fillAmount = maxTimeCatcher / gameTotalTimer;  // Normalize the time between 0 and 1
        image.fillAmount = fillAmount;

        Color color = gradient.Evaluate(fillAmount); // Gets the color for the fill
        image.color = color; // Applies the color to the image
    }
}
