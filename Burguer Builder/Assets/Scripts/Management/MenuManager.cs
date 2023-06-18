using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] menuPanels;

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Gets the gamemode
    public void SetRandomOrderMode(bool randomOrder)
    {
        gamemodeTracker.randomOrderMode = randomOrder;
    }

}
