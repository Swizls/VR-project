using SOData;
using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

[RequireComponent(typeof(CraftRecipesContainer))]
public class SelectedCraftRenderer : MonoBehaviour
{
    [SerializeField] private ResourcePresenterElement _uiElementPrefab;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private RectTransform _uiContainer;

    private List<GameObject> _createdUIElements = new List<GameObject>();

    private CraftRecipesContainer _craftContainer;
    private CraftRecipe _selectedCraftRecipe;

    private void Start()
    {
        _craftContainer = GetComponent<CraftRecipesContainer>();
        _craftContainer.CraftRecipeChanged += Render;

        if (_craftContainer.SelectedCraftRecipe != null)
            Render(_craftContainer.SelectedCraftRecipe);
    }

    private void OnDisable()
    {
        _craftContainer.CraftRecipeChanged -= Render;
    }

    private void Render(CraftRecipe recipe)
    {
        Clear();
        _itemNameText.text = recipe.RecipeName;


        foreach (var item in recipe.RequiredResources.ToDictionary())
        {
            ResourcePresenterElement element = Instantiate(_uiElementPrefab, _uiContainer.transform);
            element.SetValues(item.Key, item.Value);
            _createdUIElements.Add(element.gameObject);
        }
    }
    private void Clear()
    {
        foreach(var item in _createdUIElements) 
        {
            Destroy(item);
        }
        _createdUIElements.Clear();
    }
}
