using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichBuilder : MonoBehaviour
{
    public List<Ingredient> selectedIngredients = new List<Ingredient>();
    [SerializeField] private GameObject topBunPrefab;
    [SerializeField] private GameObject bottomBunPrefab;

    [SerializeField] private float ingredientYOffset;
    [SerializeField] private float ingredientOffsetValue;

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
        }
        else
        {
            CheckSandwich();
        }
    }

    private void InstantiateBuns()
    {
        GameObject topBunGO = Instantiate(topBunPrefab, transform);
        GameObject bottomBunGO = Instantiate(bottomBunPrefab, transform);
    }

    public void CheckSandwich()
    {
        if (selectedIngredients.Count == requiredIngredients)
        {
            Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
            
            if (CheckIngredientsMatch(currentSandwich))
            {
                //* The sandwich is correct
                // Gamemanager.instance.AddPoints();
                // Throw the sandwich to the right;
            }
            else
            {
                //* The sandwich is incorrect
                // Gamemanager.instance.RemovePoints();
                // Throw the sandwich to the left;
            }

            // Clear the ingredient list for the next sandwich
            selectedIngredients.Clear();

            // Show the next sandwich
            SandwichManager.instance.DisplayNextSandwich();
        }
    }

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

}
