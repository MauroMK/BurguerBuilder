using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichManager : MonoBehaviour
{
    public static SandwichManager instance;
    public int currentSandwichIndex = 0;

    private SandwichUIManager uiManager;

    public List<Sandwich> sandwiches;
    
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

    void Start()
    {
        uiManager = FindObjectOfType<SandwichUIManager>();
        ShuffleSandwiches();
    }

    void ShuffleSandwiches()
    {
        // Shuffles the sandwich list
        int count = sandwiches.Count;
        for (int i = 0; i < count - 1; i++)
        {
            int randomIndex = Random.Range(i, count);
            Sandwich temp = sandwiches[randomIndex];
            sandwiches[randomIndex] = sandwiches[i];
            sandwiches[i] = temp;
        }
        
        // Reset the current sandwich index to zero
        currentSandwichIndex = 0;

        // Get the current sandwich after shuffling
        Sandwich currentSandwich = GetCurrentSandwich();
        uiManager.UpdateSandwichUI(currentSandwich);
    }

    public Sandwich GetCurrentSandwich()
    {
        if (currentSandwichIndex >= 0 && currentSandwichIndex < sandwiches.Count)
        {
            return sandwiches[currentSandwichIndex];
        }

        return null;
    }

    public void DisplaySandwichUI()
    {
        if (currentSandwichIndex < sandwiches.Count)
        {
            // Next sandwich
            currentSandwichIndex++;

            Sandwich currentSandwich = sandwiches[currentSandwichIndex];

            // Refresh the UI with the current sandwich information (name, icon, ingredients)
            uiManager.UpdateSandwichUI(currentSandwich);
        }
    }
}
