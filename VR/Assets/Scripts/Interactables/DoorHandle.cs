using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private DoorGrabbable _invisibleHandle;

    private bool _isDragging = false;

    private Rigidbody _rigibody;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _invisibleHandle.DragHandleStart += OnDragStart;
        _invisibleHandle.DragHandleEnd += OnDragStop;
    }

    private void OnDisable()
    {
        _invisibleHandle.DragHandleStart -= OnDragStart;
        _invisibleHandle.DragHandleEnd -= OnDragStop;
    }

    private void FixedUpdate()
    {
        if (_isDragging) 
            _rigibody.MovePosition(_target.position);
    }
    private void OnDragStart() => _isDragging = true;
    private void OnDragStop() => _isDragging = false;
}
