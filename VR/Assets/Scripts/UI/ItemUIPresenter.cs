using TMPro;
using UnityEngine;

public class ItemUIPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private BaseItem _item;
    private InventoryRenderer _inventory;

    private void Start()
    {
        if (_textMeshPro is null)
            throw new System.ArgumentNullException();
    }

    public void Initialize(BaseItem item, InventoryRenderer inventoryReference)
    {
        _item = item;
        _inventory = inventoryReference;
        SetItemName();
    }
    public void RemoveItem()
    {
        _inventory.RemoveItem(_item, gameObject);
    }

    private void SetItemName()
    {
        _textMeshPro.text = _item.Name;
    }
}
