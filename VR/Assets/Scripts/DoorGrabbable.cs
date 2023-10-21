using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorGrabbable : XRGrabInteractable
{
    [SerializeField] private Transform _handler;

    protected override void Detach()
    {
        base.Detach();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        Rigidbody handlerRigidbody = _handler.GetComponent<Rigidbody>();
        handlerRigidbody.velocity = Vector3.zero;
        handlerRigidbody.angularVelocity = Vector3.zero;
    }
}
