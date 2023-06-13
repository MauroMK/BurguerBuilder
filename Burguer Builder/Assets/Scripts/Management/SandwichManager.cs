using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichManager : MonoBehaviour
{
    public static SandwichManager instance;

    public List<Sandwich> sandwiches;
    
    [SerializeField] private int currentSandwichIndex = 0;

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
    }

    public Sandwich GetCurrentSandwich()
    {
        if (currentSandwichIndex >= 0 && currentSandwichIndex < sandwiches.Count)
        {
            return sandwiches[currentSandwichIndex];
        }

        return null;
    }

    public void DisplayNextSandwich()
    {
        if (currentSandwichIndex < sandwiches.Count)
        {
            Sandwich currentSandwich = sandwiches[currentSandwichIndex];
            sandwiches.RemoveAt(currentSandwichIndex);

            // Refresh the UI with the current sandwich information (name, icon, ingredients)

            // next sandwich
            currentSandwichIndex++;
        }
        else
        {
            // All sandwiches were shown
            // Show total points and the restart button
        }
    }
}
