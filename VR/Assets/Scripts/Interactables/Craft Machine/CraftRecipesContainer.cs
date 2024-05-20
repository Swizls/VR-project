using SOData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipesContainer : MonoBehaviour
{
    [SerializeField] private List<CraftRecipe> _availableCraftRecipies;
    [SerializeField] private CraftRecipe _selectedCraftRecipe;

    public Action<CraftRecipe> CraftRecipeChanged;

    public List<CraftRecipe> AvailableCraftRecipe => _availableCraftRecipies;
    public CraftRecipe SelectedCraftRecipe => _selectedCraftRecipe;

    private void Start()
    {
        if (_selectedCraftRecipe != null)
            return;

        if (_availableCraftRecipies != null && _availableCraftRecipies.Count > 0)
            _selectedCraftRecipe = _availableCraftRecipies[0];
    }

    public void SelectCraftRecipe(CraftRecipe recipe)
    {
        _selectedCraftRecipe = recipe;
        CraftRecipeChanged?.Invoke(recipe);
    }

}
