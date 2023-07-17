using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] menuPanels;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text percentageText;

    private float fadeSpeed = 0.5f;
    private GamemodeTracker gamemodeTracker;

    private void Start() 
    {
        gamemodeTracker = FindObjectOfType<GamemodeTracker>();
    }

    #region Menuhandler
    private void ShowMenu(CanvasGroup menu)
    {
        LeanTween.alphaCanvas(menu, 1, fadeSpeed);
        menu.interactable = true;
        menu.blocksRaycasts = true;
    }

    private void HideMenu(CanvasGroup menu)
    {
        LeanTween.alphaCanvas(menu, 0, fadeSpeed);
        menu.interactable = false;
        menu.blocksRaycasts = false;
    }

    public void SwitchMenu(CanvasGroup targetMenu)
    {
        foreach (CanvasGroup menu in menuPanels)
        {
            if (menu == targetMenu)
            {
                ShowMenu(menu);
            }
            else
            {
                HideMenu(menu);
            }
        }
    }
    #endregion

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(loadingScreen);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            percentageText.text = progress * 100f + "%";

            yield return null;
        }
    }

    // Gets the gamemode
    public void SetRandomOrderMode(bool randomOrder)
    {
        gamemodeTracker.randomOrderMode = randomOrder;
    }

}
