using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sandwich", menuName = "Sandwiches/Sandwich")]
public class Sandwich : ScriptableObject 
{
    [SerializeField] private string sandwichName;
    [SerializeField] private Sprite sandwichIcon;
    [SerializeField] private Ingredient[] ingredients;
}

