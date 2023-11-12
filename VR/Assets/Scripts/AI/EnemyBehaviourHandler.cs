using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    [RequireComponent(typeof(FieldOfView))]
    public class EnemyBehaviourHandler : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject _warningIcon;

        private Vector3 _startPosition;

        private NavMeshAgent _agent;
        private TestNavMeshMover _testNavMeshMover;
        private FieldOfView _fieldOfView;

        public NavMeshAgent Agent => _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _testNavMeshMover = GetComponent<TestNavMeshMover>();
            _fieldOfView = GetComponent<FieldOfView>();

            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _fieldOfView.PlayerHasBeenSpotted += OnPlayerAppearence;
            _fieldOfView.PlayerHasBeenLost += OnPlayerContactLost;
        }

        private void OnDisable()
        {
            if (_fieldOfView == null)
                return;

            _fieldOfView.PlayerHasBeenSpotted -= OnPlayerAppearence;
            _fieldOfView.PlayerHasBeenLost -= OnPlayerContactLost;

            _warningIcon.SetActive(false);
        }

        private void OnPlayerAppearence(Vector3 playerPosition)
        {
            _warningIcon.SetActive(true);
            _agent.SetDestination(playerPosition);
        }

        private void OnPlayerContactLost()
        {
            _warningIcon.SetActive(false);
            if (_testNavMeshMover.StartWaypoint != null)
                _agent.SetDestination(_testNavMeshMover.StartWaypoint.transform.position);
            else
                Idle();
        }

        private void Idle()
        {
            _agent.SetDestination(_startPosition);
        }
    }
}
