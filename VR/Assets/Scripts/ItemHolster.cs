using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemHolster : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _holdingItem;
    private IEnumerator HoldItem()
    {
        while (!_holdingItem.isSelected)
        {
            yield return null;
        }
        DropItem();
    }

    private void GrabItem(XRGrabInteractable item)
    {
        _holdingItem = item;

        _holdingItem.transform.SetParent(transform);

        _holdingItem.transform.localPosition = Vector3.zero;
        _holdingItem.transform.localRotation = Quaternion.identity;

        _holdingItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DropItem()
    {
        _holdingItem.GetComponent<Rigidbody>().isKinematic = false;
        _holdingItem.transform.parent = null;
        _holdingItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_holdingItem != null)
        {
            return;
        }

        XRGrabInteractable item = other.GetComponentInParent<XRGrabInteractable>();

        if (item != null && !item.isSelected)
        {
            GrabItem(item);
            StartCoroutine(HoldItem());
        }
    }
}
