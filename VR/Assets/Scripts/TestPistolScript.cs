using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TestPistolScript : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Fire);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward, Color.green);
    }

    private void Fire(ActivateEventArgs arg0)
    {
        Debug.Log("Shoot");
        Physics.Raycast(_firePoint.position, _firePoint.forward, out RaycastHit hit);
        if(hit.collider != null)
            hit.collider.gameObject.SetActive(false);
    }
}
