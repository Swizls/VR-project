using UnityEngine;

public class ItemRecivier : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BaseItem>(out BaseItem item))
        {
            _inventory.AddItem(item);
            return;
        }

        item = other.GetComponentInParent<BaseItem>();

        if (item != null)
        {
            _inventory.AddItem(item);
        }
    }
}
