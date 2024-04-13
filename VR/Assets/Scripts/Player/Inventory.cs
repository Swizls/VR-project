using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private Transform _rightHandTransform;
    
    public List<Item> Items => _items;

    public Action<Item> InventoryUpdated;

    private void Start()
    {
        List<Item> createdItems = new List<Item>();
        foreach (var item in _items)
        {
            Item createdItem = Instantiate(item);
            createdItems.Add(createdItem);
            createdItem.Hide();
        }
        _items = createdItems;
    }

    public void AddItem(Item item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _items.Add(item);
        item.Hide();

        InventoryUpdated?.Invoke(item);
    }

    public void RemoveItem(Item item) 
    { 
        _items.Remove(item);
        item.Display(_rightHandTransform);

        InventoryUpdated?.Invoke(item);
    }
}
