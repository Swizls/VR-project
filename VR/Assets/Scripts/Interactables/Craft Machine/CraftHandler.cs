using SOData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CraftRecipesContainer))]
public class CraftHandler : MonoBehaviour
{
    [SerializeField] private ResourceBankPresenter _resourceBankPresenter;
    [SerializeField] private Transform _spawnPointTransform;
    private CraftRecipesContainer _recipesContainer;

    private void Start()
    {
        _recipesContainer = GetComponent<CraftRecipesContainer>();
    }

    public void Craft()
    {
        Dictionary<string, int> requiredResources = _recipesContainer.SelectedCraftRecipe.RequiredResources.ToDictionary();

        if (!IsEnoughResources(requiredResources))
            return;

        Instantiate(_recipesContainer.SelectedCraftRecipe.ItemPrefab, _spawnPointTransform.position, _spawnPointTransform.rotation);
        DeductResources(requiredResources);
    }

    private void DeductResources(Dictionary<string, int> requiredResources)
    {
        for(int i = 0; i < requiredResources.Count; i++) 
        {
            _resourceBankPresenter.Bank.Remove(requiredResources.ElementAt(i).Key, requiredResources.ElementAt(i).Value);
        }
    }

    private bool IsEnoughResources(Dictionary<string, int> requiredResources)
    {
        for (int i = 0; i < requiredResources.Count; i++)
        {
            if (requiredResources.Count > _resourceBankPresenter.Bank.Resources.Count)
                return false;

            KeyValuePair<string, int> dictionaryLine = requiredResources.ElementAt(i);

            if (dictionaryLine.Value > _resourceBankPresenter.Bank.Resources[dictionaryLine.Key])
                return false;
        }
        return true;
    }
}