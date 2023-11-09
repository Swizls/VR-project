using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMeshMover : MonoBehaviour
{
    [SerializeField] Waypoint _startWaypoint;

    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (_agent == null || _startWaypoint == null)
            return;

        _agent.SetDestination(_startWaypoint.transform.position);
    }
}
