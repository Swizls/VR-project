using UnityEngine;

[RequireComponent(typeof(KeycardReader))]
public class KeycardReaderStatus : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _openDoorStatusColor;
    [SerializeField] private Color _lockedDoorStatusColor;

    [SerializeField] private Light _lightSource; 
    [SerializeField] private GameObject _statusDisplay;

    private KeycardReader _keycardReader;
    private Renderer _cachedRenderer;

    #region MONO
    private void Start()
    {
        _keycardReader = GetComponent<KeycardReader>();
        _cachedRenderer = _statusDisplay.GetComponent<Renderer>();

        _keycardReader.LockStatusChanged += SetStatus;

        SetStatus(_keycardReader.IsAirlockLocked);
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

    #endregion

    private void SetStatus(bool status)
    {
        if (status)
        {
            _cachedRenderer.material.color = _lockedDoorStatusColor;
            _lightSource.color = _lockedDoorStatusColor;
        }
        else
        {
            _lightSource.color = _openDoorStatusColor;
            _cachedRenderer.material.color = _openDoorStatusColor;
        }
    }
}
