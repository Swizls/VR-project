using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Rigidbody _rigibody;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigibody.MovePosition(_target.position);
    }
}
