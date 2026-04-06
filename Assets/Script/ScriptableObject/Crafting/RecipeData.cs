using System;
using UnityEngine;

[Serializable]
public struct CrafIngredient
{
    public ValueItem item;
    public int amount;
}


[CreateAssetMenu(fileName = "RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public CrafIngredient[] itemNeeded;

    public CrafIngredient product;
}
