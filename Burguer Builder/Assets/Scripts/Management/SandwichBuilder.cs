using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichBuilder : MonoBehaviour
{
    [Header("Bun Prefabs")]
    [SerializeField] private GameObject topBunPrefab;
    [SerializeField] private GameObject bottomBunPrefab;
    private GameObject topBunGO;
    private GameObject bottomBunGO;

    [Header("Ingredients position")]
    [SerializeField] private float ingredientYOffset;
    [SerializeField] private float ingredientOffsetValue;

    [Header("Ingredient List")]
    public List<Ingredient> selectedIngredients = new List<Ingredient>();

    private int requiredIngredients = 3;

    public void OnIngredientButtonClick(Ingredient ingredient)
    {
        if (selectedIngredients.Count == 0)
        {
            InstantiateBuns();
            AddIngredient(ingredient);
        }
        else
        {
            AddIngredient(ingredient);
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

            // Sets the vertical position of the ingredient based on the index
            float ingredientYPosition = (selectedIngredients.Count - ingredientYOffset) * ingredientOffsetValue;;
            
            // Adjusts the vertical position of the ingredient
            Vector3 ingredientPosition = ingredientGO.transform.position;
            ingredientPosition.y = ingredientYPosition;
            ingredientGO.transform.position = ingredientPosition;
        
            if (selectedIngredients.Count == requiredIngredients)
            {
                CheckSandwich();
            }
        }
    }

    private void InstantiateBuns()
    {
        this.topBunGO = Instantiate(topBunPrefab, transform);
        this.bottomBunGO = Instantiate(bottomBunPrefab, transform);
    }

    public void CheckSandwich()
    {
        if (selectedIngredients.Count == requiredIngredients)
        {
            ClearIngredientsAndBuns();

            Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
            
            if (CheckIngredientsMatch(currentSandwich))
            {
                //* The sandwich is correct
                GameManager.instance.AddPoints(20);
                // Throw the sandwich to the right;
            }
            else
            {
                //* The sandwich is incorrect
                GameManager.instance.RemovePoints(10);
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
    }

}
