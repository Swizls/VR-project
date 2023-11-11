using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _nextWaypoint;

    public GameObject NextWaypoint => _nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out TestNavMeshMover agent))
        {
            agent.Agent.SetDestination(_nextWaypoint.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _nextWaypoint.transform.position);
    }
}
