using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemReceiver : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void OnTriggerStay(Collider other)
    {
        XRGrabInteractable grabbedItem = other.GetComponent<XRGrabInteractable>();

        if(grabbedItem == null)
            grabbedItem = other.GetComponentInParent<XRGrabInteractable>();

        if (grabbedItem.isSelected)
            return;

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
