using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup dificultySelectionPanel;
    [SerializeField] private CanvasGroup settingsPanel;

    private float fadeSpeed = 0.5f;
    private GamemodeTracker gamemodeTracker;

    private void Start() 
    {
        gamemodeTracker = FindObjectOfType<GamemodeTracker>();
    }

    #region Menuhandler
    public void ToDificultySelection()
    {
        HideMainMenu();
        Invoke("ShowDificultySelection", fadeSpeed);
    }

    public void ToMainMenuFromDificulty()
    {
        HideDificultySelection();
        Invoke("ShowMainMenu", fadeSpeed);
    }

    public void ToMainMenuFromSettings()
    {
        HideSettingsMenu();
        Invoke("ShowMainMenu", fadeSpeed);
    }

    public void ToSettingsMenu()
    {
        HideMainMenu();
        Invoke("ShowSettingsMenu", fadeSpeed);
    }

    //* Main Menu
    private void ShowMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuPanel, 1, fadeSpeed);
        mainMenuPanel.interactable = true;
        mainMenuPanel.blocksRaycasts = true;
    }

    private void HideMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuPanel, 0, fadeSpeed);
        mainMenuPanel.interactable = false;
        mainMenuPanel.blocksRaycasts = false;
    }

    //* Dificulty Selection
    private void ShowDificultySelection()
    {
        LeanTween.alphaCanvas(dificultySelectionPanel, 1, fadeSpeed);
        dificultySelectionPanel.interactable = true;
        dificultySelectionPanel.blocksRaycasts = true;
    }

    private void HideDificultySelection()
    {
        LeanTween.alphaCanvas(dificultySelectionPanel, 0, fadeSpeed);
        dificultySelectionPanel.interactable = false;
        dificultySelectionPanel.blocksRaycasts = false;
    }

    //* Settings Menu
    private void ShowSettingsMenu()
    {
        LeanTween.alphaCanvas(settingsPanel, 1, fadeSpeed);
        settingsPanel.interactable = true;
        settingsPanel.blocksRaycasts = true;
    }

    private void HideSettingsMenu()
    {
        LeanTween.alphaCanvas(settingsPanel, 0, fadeSpeed);
        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
    }
    #endregion

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetRandomOrderMode(bool randomOrder)
    {
        gamemodeTracker.randomOrderMode = randomOrder;
    }

}
