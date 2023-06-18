using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichBuilder : MonoBehaviour
{
    [Header("Bun Prefabs")]
    [SerializeField] private GameObject topBunPrefab;
    [SerializeField] private GameObject bottomBunPrefab;
    
    [Header("Ingredient List")]
    [SerializeField] private List<Ingredient> selectedIngredients = new List<Ingredient>();

    #region Combo
    private int sandwichPointAdd = 20;
    private int sandwichPointRemove = 10;
    private int correctSandwichCount = 0;
    private int comboMultiplier = 5;
    public int comboCount = 0;
    public float comboTimer = 0f;
    private float comboTimeLimit = 5f;
    #endregion

    private GameObject topBunGO;
    private GameObject bottomBunGO;
    private float bunOffset = 0.030f;
    private int requiredIngredients = 3;
    private bool buttonsEnabled = true;

    private MoveObjectX hydraulicPressX;
    private MoveObjectZ hydraulicPressZ;
    private GamemodeTracker gameMode;

    private void Start() 
    {
        hydraulicPressX = FindObjectOfType<MoveObjectX>();
        hydraulicPressZ = FindObjectOfType<MoveObjectZ>();
        gameMode = FindObjectOfType<GamemodeTracker>();
    }

    private void Update() 
    {
        ComboTimerHandler();
    }

    public void OnIngredientButtonClick(Ingredient ingredient)
    {
        if (buttonsEnabled)
        {
            if (selectedIngredients.Count == 0)
            {
                InstantiateBottomBun();
                AddIngredient(ingredient);
            }
            else
            {
                AddIngredient(ingredient);
            }
        }

    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (selectedIngredients.Count < requiredIngredients)
        {
            // Add the ingredient
            selectedIngredients.Add(ingredient);

            // Instantiates the prefab on the scene
            GameObject ingredientGO = Instantiate(ingredient.ingredientPrefab, transform);

            AudioManager.instance.PlaySound("Pop");
        
            if (selectedIngredients.Count == requiredIngredients)
            {
                InstantiateTopBun();
                CheckSandwich();
            }
        }
    }

    private void InstantiateTopBun()
    {
        this.topBunGO = Instantiate(topBunPrefab, transform);

        //* Ajust the local position for the bun when it spawns, so it doesn't get stuck into other ingredient
        Vector3 topBunPosition = this.topBunGO.transform.localPosition;
        topBunPosition.y += bunOffset;
        this.topBunGO.transform.localPosition = topBunPosition;
    }

    private void InstantiateBottomBun()
    {
        this.bottomBunGO = Instantiate(bottomBunPrefab, transform);

        //* Ajust the local position for the bun when it spawns, so it doesn't get stuck into other ingredient
        Vector3 bottomBunPosition = this.bottomBunGO.transform.localPosition;
        bottomBunPosition.y -= bunOffset;
        this.bottomBunGO.transform.localPosition = bottomBunPosition;
    }

    public void CheckSandwich()
    {
        if (selectedIngredients.Count == requiredIngredients)
        {
            DisableButtons();

            // Calls the method after 1 second to have time until the sandwich is full
            Invoke("ClearIngredientsAndBuns", 1f);

            Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
            
            //* Gamemode Correct Order
            if (gameMode.randomOrderMode == false)
            {
                if (CheckIngredientsMatch(currentSandwich))
                {
                    MadeCorrectSandwich();
                }
                else
                {
                    MadeWrongSandwich();
                }
            }

            //* Gamemode Random Order
            if (gameMode.randomOrderMode == true)
            {
                if (CheckIngredientsRamdomly(currentSandwich))
                {
                    MadeCorrectSandwich();
                }
                else
                {
                    MadeWrongSandwich();
                }
            }

            // Clear the ingredient list for the next sandwich
            selectedIngredients.Clear();

            // Show the next sandwich
            SandwichManager.instance.DisplaySandwichUI();

        }
    }

    //* Checks if the ingredients where chosen in the correct order
    private bool CheckIngredientsMatch(Sandwich sandwich)
    {
        List<Ingredient> sandwichIngredients = sandwich.ingredients;

        if (selectedIngredients.Count != sandwichIngredients.Count)
        {
            return false;
        }

        for (int i = 0; i < selectedIngredients.Count; i++)
        {
            if (!selectedIngredients[i].Equals(sandwichIngredients[i]))
            {
                return false;
            }
        }

        return true;
    }

    //* Checks if the ingredients where chosen randomly
    private bool CheckIngredientsRamdomly(Sandwich sandwich)
    {
        List<Ingredient> sandwichIngredients = sandwich.ingredients;

        if (selectedIngredients.Count != sandwichIngredients.Count)
        {
            return false;
        }

        foreach (Ingredient ingredient in sandwichIngredients)
        {
            if (!selectedIngredients.Contains(ingredient))
            {
                return false;
            }
        }

        foreach (Ingredient ingredient in selectedIngredients)
        {
            if (!sandwichIngredients.Contains(ingredient))
            {
                return false;
            }
        }

        return true;
    }

    private void ClearIngredientsAndBuns()
    {
        foreach (Transform child in transform)
        {
            if (child != transform && child.gameObject != this.topBunGO && child.gameObject != this.bottomBunGO)
            {
                Destroy(child.gameObject);
            }
        }

        // Destroys the buns
        if (topBunGO != null)
        {
            Destroy(topBunGO);
        }

        if (bottomBunGO != null)
        {
            Destroy(bottomBunGO);
        }

        //* ENABLES THE UI TO ADD MORE INGREDIENTS
        EnableButtons();
    }

    private void DisableButtons()
    {
        buttonsEnabled = false;
    }

    private void EnableButtons()
    {
        buttonsEnabled = true;
    }

    private void MadeCorrectSandwich()
    {
        // Adds points to the GameManager based on the combo counter
        // If the combo counter is greater than zero, adds the sandwich value plus the combo multiplier
        // Otherwise, adds only the sandwich value
        GameManager.instance.AddPoints(comboCount >= 5 ? sandwichPointAdd + (comboMultiplier * (comboCount / 5)) : sandwichPointAdd);
        hydraulicPressZ.MoveCorrectSandwich();  // Throw the sandwich to the delivery box

        // Increase the sandwich counter and the combo counter
        correctSandwichCount++;
        comboCount++;

        // Restart the combo timer
        comboTimer = comboTimeLimit;

        if (correctSandwichCount % 5 == 0 && comboCount > 0)
        {
            // Increase the sandwich value for every 5 sandwiches made correct
            sandwichPointAdd += 5;
        }
    }

    private void MadeWrongSandwich()
    {
        GameManager.instance.RemovePoints(sandwichPointRemove);
        hydraulicPressX.MoveWrongSandwich();   // Throw the sandwich to the trash;

        // Reset the combo counter
        comboCount = 0;
        correctSandwichCount = 0;

        // Reset the sandwich value back to the original
        sandwichPointAdd = 20;
    }

    private void ComboTimerHandler()
    {
        // Updates the combo timer
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0f)
            {
                // Combo time is over, reset the values
                comboCount = 0;
                correctSandwichCount = 0;
                sandwichPointAdd = 20;
            }
        }
    }

}
