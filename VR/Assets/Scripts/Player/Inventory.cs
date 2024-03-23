using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<BaseItem> _items;
    [SerializeField] private Transform _rightHandTransform;

    public List<BaseItem> Items => _items;

    public Action<BaseItem> InventoryUpdated;

    private void Start()
    {
        foreach (var item in _items)
        {
            if(item.gameObject is null)
            {
                Instantiate(item);
            }
            item.Hide();
        }
    }

    public void AddItem(BaseItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _items.Add(item);
        item.Hide();

        InventoryUpdated?.Invoke(item);
    }

    public void RemoveItem(BaseItem item) 
    { 
        _items.Remove(item);
        item.Display(_rightHandTransform);

        InventoryUpdated?.Invoke(item);
    }
}
