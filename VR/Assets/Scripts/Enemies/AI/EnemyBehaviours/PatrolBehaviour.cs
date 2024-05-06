using Game.Enemies.AI.Utilites;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemies.AI
{ 
    public class PatrolBehaviour : EnemyBehaviour
    {
        private const string ANIMATION_NAME = "Walking";

        private Waypoint _targetWaypoint;
        public PatrolBehaviour(EnemyBehaviourHandler enemyReference) : base(enemyReference)
        {
            _animationName = ANIMATION_NAME;
            _canBeUpdated = true;
        }
    
        public override void Enter()
        {
            _animationName = ANIMATION_NAME;
            _enemyReference.EnemyMover.StartMoving();
        }

        public override void Update()
        {
            if (_enemyReference.EnemyMover.Agent.remainingDistance > 1f)
                return;

            _targetWaypoint = GetNextWaypoint();
            MoveToWaypoint();
        }
    
        public override void Exit()
        {
            _enemyReference.EnemyMover.StopMoving();
        }

        private Waypoint GetNextWaypoint()
        {
            List<Waypoint> waypoints = _enemyReference.WaypointCointainer.Waypoints.ToList();
            int waypointsCount = waypoints.Count;
            Waypoint waypoint = null;

            for (int i = 0; i < waypointsCount; i++)
            {
                waypoint = waypoints[Random.Range(0, waypoints.Count - 1)];
                NavMeshPath path = new NavMeshPath();
                _enemyReference.EnemyMover.Agent.CalculatePath(waypoint.transform.position, path);

                if (path.status == NavMeshPathStatus.PathComplete)
                    return waypoint;
                else
                    waypoints.Remove(waypoint);
            }

            return null;
        }

        private void MoveToWaypoint()
        {
            _enemyReference.EnemyMover.Agent.SetDestination(_targetWaypoint.transform.position);
        }
    }
}