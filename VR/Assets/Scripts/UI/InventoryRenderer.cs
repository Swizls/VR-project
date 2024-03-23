using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryItemUIPrefab;

    private List<GameObject> _itemsUI = new List<GameObject>();

    private void Start()
    {
        if (_inventory is null)
            throw new System.ArgumentNullException(nameof(Inventory) + "is null");

        if (_inventoryItemUIPrefab is null)
            throw new System.ArgumentNullException();
    }

    private void OnEnable()
    {
        Render();
    }

    private void OnDisable()
    {
        Clear();
    }

    private void Clear()
    {
        foreach (var item in _itemsUI)
        {
            Destroy(item.gameObject);
        }
        _itemsUI.Clear();
    }

    private void Render()
    {
        foreach(BaseItem item in _inventory.Items)
        {
            var createdUIItem = Instantiate(_inventoryItemUIPrefab, gameObject.transform);
            createdUIItem.GetComponent<ItemUIPresenter>().Initialize(item, this);
            _itemsUI.Add(createdUIItem);
        }
    }

    public void RemoveItem(BaseItem item, GameObject itemPresenter)
    {
        item.gameObject.SetActive(true);
        _itemsUI.Remove(itemPresenter);
        Destroy(itemPresenter);
        _inventory.RemoveItem(item);
    }
}
