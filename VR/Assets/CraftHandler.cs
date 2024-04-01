using SOData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftHandler : MonoBehaviour
{
    [SerializeField] private ResourceBankPresenter _resourceBankPresenter;
    [SerializeField] private CraftRecipe _selectedCraftRecipe;
    [SerializeField] private Transform _spawnPointTransform;

    public void Craft()
    {
        Dictionary<string, int> requiredResources = _selectedCraftRecipe.RequiredResources.ToDictionary();

        if (!CheckIsEnoughResources(requiredResources))
            return;

        Instantiate(_selectedCraftRecipe.ItemPrefab, _spawnPointTransform.position, _spawnPointTransform.rotation);
        DeductResources(requiredResources);
    }

    private void DeductResources(Dictionary<string, int> requiredResources)
    {
        for(int i = 0; i < requiredResources.Count; i++) 
        {
            _resourceBankPresenter.Bank.Remove(requiredResources.ElementAt(i).Key, requiredResources.ElementAt(i).Value);
        }
    }

    private bool CheckIsEnoughResources(Dictionary<string, int> requiredResources)
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