using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sandwich", menuName = "Sandwiches/Sandwich")]
public class Sandwich : ScriptableObject 
{
    public string sandwichName;
    public Sprite sandwichIcon;
    public List<Ingredient> ingredients;
}

