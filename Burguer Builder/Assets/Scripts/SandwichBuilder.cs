using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichBuilder : MonoBehaviour
{
    public List<Ingredient> selectedIngredients = new List<Ingredient>();

    private int requiredIngredients = 3;

    public void AddIngredient(Ingredient ingredient)
    {
        if (selectedIngredients.Count < requiredIngredients)
        {
            // Add the ingredient
            selectedIngredients.Add(ingredient);

            // Instantiates the prefab on the scene
            GameObject ingredientGO = Instantiate(ingredient.ingredientPrefab, transform);

            //TODO Ajust the position on the sandwich
        }
        else
        {
            Debug.Log("Limite de ingredientes atingido!");
        }
    }

    public void OnIngredientButtonClick(Ingredient ingredient)
    {
        if (selectedIngredients.Count == 0)
        {
            Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
            selectedIngredients.Add(currentSandwich.topBunPrefab.GetComponent<Ingredient>());
            selectedIngredients.Add(ingredient);
            selectedIngredients.Add(currentSandwich.bottomBunPrefab.GetComponent<Ingredient>());
        }
        else
        {
            AddIngredient(ingredient);
        }
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
