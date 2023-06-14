using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ingredient", menuName = "Sandwiches/Ingredient")]
public class Ingredient : ScriptableObject 
{
    [SerializeField] private string ingredientName;
    public Sprite icon;
    public GameObject ingredientPrefab;
}
