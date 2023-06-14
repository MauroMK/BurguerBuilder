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
        // Exibe o sanduíche ativo no início do jogo
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

        // Adiciona os ícones dos ingredientes do sanduíche à UI
        foreach (Ingredient ingredient in sandwich.ingredients)
        {
            // Cria um novo Image para o ingrediente
            GameObject ingredientImageObj = new GameObject("IngredientImage", typeof(RectTransform), typeof(Image));
            
            // Define o transform parent como o ingredientContainer
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
