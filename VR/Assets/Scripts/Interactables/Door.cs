using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorObject;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private bool _isLocked;

    private Vector3 _doorSavedPosition;
    private Quaternion _doorSavedRotation;

    private Rigidbody _rigidbody;

    public bool IsLocked => _isLocked;

    private void Start()
    {
        _rigidbody = _doorObject.GetComponent<Rigidbody>();

        _doorSavedPosition = _doorObject.transform.localPosition;
        _doorSavedRotation = _doorObject.transform.localRotation;

        if (_isLocked) 
        {
            if (_doorObject == null)
                throw new System.NullReferenceException();

            SetDoorLock();
        }
    }

    private void SetDoorLock()
    {
        _rigidbody.isKinematic = _isLocked;

        _doorObject.transform.localPosition = _doorSavedPosition;
        _doorObject.transform.localRotation = _doorSavedRotation;
    }

    public void ToggleLockValue()
    {
        _isLocked = !_isLocked;
        SetDoorLock();
    }
}
