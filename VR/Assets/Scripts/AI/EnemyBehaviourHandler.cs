using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI
{
    [RequireComponent(typeof(FieldOfView))]
    public class EnemyBehaviourHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _warningIcon;

        private NavMeshAgent _agent;
        private FieldOfView _fieldOfView;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _fieldOfView = GetComponent<FieldOfView>();

            _fieldOfView.PlayerHasBeenSpotted += FollowPlayer;
            _fieldOfView.PlayerHasBeenLost += Patrol;
        }

        private void OnDisable()
        {
            if (_fieldOfView == null)
                return;

            _fieldOfView.PlayerHasBeenSpotted -= FollowPlayer;
            _fieldOfView.PlayerHasBeenLost -= Patrol;

            _warningIcon.SetActive(false);
        }

        private void FollowPlayer(Vector3 playerPosition)
        {
            _warningIcon.SetActive(true);
            _agent.SetDestination(playerPosition);
        }

        private void Patrol()
        {
            _warningIcon.SetActive(false);
            Waypoint waypoint = FindObjectOfType<Waypoint>();
            _agent.SetDestination(waypoint.transform.position);
        }
    }
}
