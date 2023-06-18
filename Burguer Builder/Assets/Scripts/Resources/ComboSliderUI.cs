using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboSliderUI : MonoBehaviour
{
    [SerializeField] private Slider comboSlider;
    [SerializeField] private TMP_Text comboText;

    private SandwichBuilder sandwichBuilder;

    private void Start() 
    {
        sandwichBuilder = FindObjectOfType<SandwichBuilder>();
    }

    private void Update() 
    {
        UpdateComboUI();    
    }

    private void UpdateComboUI()
    {
        comboSlider.value = sandwichBuilder.comboTimer;
        comboText.text = "Combo: " + sandwichBuilder.comboCount.ToString();
    }
}
