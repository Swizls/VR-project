using UnityEngine;

public class ItemReceiver : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item))
        {
            _inventory.AddItem(item);
            return;
        }

        item = other.GetComponentInParent<Item>();

        if (item != null)
        {
            _inventory.AddItem(item);
        }
    }
}
