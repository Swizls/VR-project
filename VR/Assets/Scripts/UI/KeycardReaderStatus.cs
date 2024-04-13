using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(KeycardReader))]
public class KeycardReaderStatus : MonoBehaviour
{
    [SerializeField] private Color _openDoorStatusColor;
    [SerializeField] private Color _lockedDoorStatusColor;

    [SerializeField] private GameObject _statusDisplay;

    private KeycardReader _keycardReader;

    private Renderer _cachedRenderer;

    private bool _doorsStatus;

    private void Start()
    {
        _keycardReader = GetComponent<KeycardReader>();
        _cachedRenderer = _statusDisplay.GetComponent<Renderer>();

        _keycardReader.LockStatusChanged += SetStatus;
    }

    private void OnEnable()
    {
        if (_keycardReader != null)
            _keycardReader.LockStatusChanged += SetStatus;
    }

    private void OnDisable()
    {
        _keycardReader.LockStatusChanged -= SetStatus;
    }

    private void SetStatus(bool status)
    {
        if (status)
            _cachedRenderer.material.color = _lockedDoorStatusColor;
        else
            _cachedRenderer.material.color = _openDoorStatusColor;
    }
}
