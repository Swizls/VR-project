using System;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    [SerializeField] private Airlock _airlock;
    [SerializeField] private KeycardType _type;

    public event Action KeycardReaded;
    public event Action<bool> LockStatusChanged;

    public bool IsAirlockLocked => _airlock.IsLocked;

    #region MONO

    private void Start()
    {
        if (_airlock == null)
            throw new NullReferenceException($"Airlock is {_airlock}");

        KeycardReaded += _airlock.ToggleLock;
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeDoorLock(other);
    }

    private void OnDrawGizmos()
    {
        if (_airlock == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _airlock.transform.position);
    }
    #endregion

    private void ChangeDoorLock(Collider other)
    {
        if (!other.transform.TryGetComponent(out Keycard keycard))
            return;

        if (_type != keycard.Keycardtype)
            return;

        KeycardReaded?.Invoke();
        LockStatusChanged?.Invoke(_airlock.IsLocked);
    }
}
