using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _offset;
    void Start()
    {
        _playerTransform = FindAnyObjectByType<CharacterController>().transform;
        enabled = false;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position + Vector3.up, Time.deltaTime);
    }
    private void OnDisable()
    {
        transform.localPosition = _offset;
    }
}
