using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandwichUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text sandwichNameText;
    [SerializeField] private Image sandwichIconImage;
    [SerializeField] private Transform ingredientContainer;

    private void Start() 
    {
        // Show the active sandwich when the game starts
        Sandwich currentSandwich = SandwichManager.instance.GetCurrentSandwich();
    }

    public void UpdateSandwichUI(Sandwich sandwich)
    {
        // Updates the sandwich name
        sandwichNameText.text = sandwich.sandwichName;

        // Updates the sandwich icon
        sandwichIconImage.sprite = sandwich.sandwichIcon;

        // Remove the old images
        ClearIngredientImages();

        // Shows the images of each ingredient
        foreach (Ingredient ingredient in sandwich.ingredients)
        {
            // Creates a new image for the ingredient
            GameObject ingredientImageObj = new GameObject("IngredientImage", typeof(RectTransform), typeof(Image));
            
            // Define o transform parent como o ingredientContainer
            // Defines the ingredientContainer as the transform parent
            ingredientImageObj.transform.SetParent(ingredientContainer, false);

            Image ingredientImage = ingredientImageObj.GetComponent<Image>();
            ingredientImage.sprite = ingredient.icon;
        }
    }

    private void ClearIngredientImages()
    {
        foreach (Transform child in ingredientContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
