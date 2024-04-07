using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class ResourcePresenterHandler : MonoBehaviour
{
    [SerializeField] private ResourceBankPresenter _resourceBankPresenter;

    [SerializeField] private GameObject _resourceElementUIPrefab;
    [SerializeField] private Transform _UIContainer;

    private List<GameObject> _createdUIElements = new List<GameObject>();

    private void Start()
    {
        if (_resourceBankPresenter == null) throw new System.ArgumentNullException();
        if (_resourceElementUIPrefab == null) throw new System.ArgumentNullException();

        _resourceBankPresenter.Bank.BankValuesUpdated += Render;
    }

    private void OnDisable()
    {
        _resourceBankPresenter.Bank.BankValuesUpdated -= Render;
    }

    private void Render()
    {
        Clear();

        for(int i = 0; i < _resourceBankPresenter.Bank.Resources.Count; i++)
        {
            GameObject element = Instantiate(_resourceElementUIPrefab, _UIContainer);
            element.GetComponent<ResourcePresenterElement>().SetValues(_resourceBankPresenter.Bank.Resources.ElementAt(i).Key, 
                                                                       _resourceBankPresenter.Bank.Resources.ElementAt(i).Value);
            _createdUIElements.Add(element);
        }
    }

    private void Clear()
    {
        foreach(GameObject element in _createdUIElements)
        {
            Destroy(element);
        }
        _createdUIElements.Clear();
    }
}
