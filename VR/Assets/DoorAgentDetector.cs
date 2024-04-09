using UnityEngine;
using UnityEngine.Events;

public class DoorAgentDetector : MonoBehaviour
{
    public UnityEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        Event.Invoke();
    }
}
