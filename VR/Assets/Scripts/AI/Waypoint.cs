using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI 
{ 
    public class Waypoint : MonoBehaviour
    {
        public enum WaypointType 
        { 
            Patrol,
            Search
        }

        [SerializeField] private WaypointType _waypointType;
        [SerializeField] private GameObject _nextWaypoint;

        public GameObject NextWaypoint => _nextWaypoint;
        public WaypointType Waypointtype => _waypointType;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out EnemyMover enemyMover))
            {
                enemyMover.Agent.isStopped = true;
                enemyMover.Agent.SetDestination(_nextWaypoint.transform.position);
                enemyMover.Agent.isStopped = false;
            }
        }

        private void OnDrawGizmos()
        {
            if(_waypointType == WaypointType.Patrol)
                Gizmos.color = Color.yellow;
            else if(_waypointType == WaypointType.Search)
                Gizmos.color= Color.red;

            Gizmos.DrawSphere(transform.position, 0.1f);

            if(_nextWaypoint != null)
                Gizmos.DrawLine(transform.position, _nextWaypoint.transform.position);
        }
    }
}

