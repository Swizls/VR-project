using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EnemyAI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _startWaypoint = null;

    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;
    public Transform StartWaypoint => _startWaypoint;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (_agent == null || _startWaypoint == null)
            return;

        _agent.SetDestination(_startWaypoint.position);
    }
}
