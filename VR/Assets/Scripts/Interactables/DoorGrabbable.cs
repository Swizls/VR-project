using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorGrabbable : XRGrabInteractable
{
    [SerializeField] private Transform _handler;
    public Action DragHandleStart;
    public Action DragHandleEnd;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        DragHandleStart?.Invoke();
    }
    protected override void Detach()
    {
        base.Detach();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        Rigidbody handlerRigidbody = _handler.GetComponent<Rigidbody>();
        handlerRigidbody.velocity = Vector3.zero;
        handlerRigidbody.angularVelocity = Vector3.zero;

        DragHandleEnd?.Invoke();
    }
}
