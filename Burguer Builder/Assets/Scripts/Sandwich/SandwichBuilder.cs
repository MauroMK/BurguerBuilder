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

    private GameObject topBunGO;
    private GameObject bottomBunGO;
    private float bunOffset = 0.030f;
    private int requiredIngredients = 3;
    private bool buttonsEnabled = true;

    public void OnIngredientButtonClick(Ingredient ingredient)
    {
        if (buttonsEnabled)
        {
            AudioManager.instance.PlaySound("Click");

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

            //* Calls the method after 0.5 seconds to have time until the sandwich is full
            Invoke("ClearIngredientsAndBuns", 0.5f);

            Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
            
            //* Change later here to check if its in order or not
            if (CheckIngredientsMatch(currentSandwich))
            {
                //* The sandwich is correct
                GameManager.instance.AddPoints();
                // Throw the sandwich to the right;
            }
            else
            {
                //* The sandwich is incorrect
                GameManager.instance.RemovePoints();
                // Throw the sandwich to the left;
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

}
