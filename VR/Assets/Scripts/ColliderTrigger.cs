using System;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event Action<GameObject> Triggered;

    private void OnTriggerEnter(Collider other)
    {
        Triggered?.Invoke(other.gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
