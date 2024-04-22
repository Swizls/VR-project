using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _startWaypoint = null;

    private NavMeshAgent _agent;

    public event Action<bool> MovingStateChanged;

    public NavMeshAgent Agent => _agent;
    public Transform StartWaypoint => _startWaypoint;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (_agent == null || _startWaypoint == null)
            return;

        _agent.SetDestination(_startWaypoint.position);
    }

    public void StartMoving() => MovingStateChanged?.Invoke(true);
    public void StopMoving() => MovingStateChanged?.Invoke(false);
}
