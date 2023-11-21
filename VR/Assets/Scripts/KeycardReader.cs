using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeycardReader : MonoBehaviour
{
    [SerializeField] private List<Door> _doorsList = new List<Door>();

    public UnityEvent KeycardReaded;

    public Action<bool> LockStatusChanged;

    public List<Door> DoorsList => _doorsList;

    private void Start()
    {
        if (_doorsList.Count == 0)
            return;

        LockStatusChanged?.Invoke(_doorsList[0].IsLocked);

        foreach(Door door in _doorsList)
            KeycardReaded.AddListener(door.ToggleLockValue);
    }

    private void OnDisable()
    {
        if(_doorsList.Count == 0)
            return;

        foreach(Door door in _doorsList)
            KeycardReaded.RemoveAllListeners();
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeDoorLock(other);
    }

    private void ChangeDoorLock(Collider other)
    {
        if (!other.transform.parent.TryGetComponent(out Keycard keycard))
            return;

        KeycardReaded?.Invoke();
        LockStatusChanged?.Invoke(_doorsList[0].IsLocked);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (Door door in _doorsList)
            Gizmos.DrawLine(transform.position, door.transform.position);
    }
}
