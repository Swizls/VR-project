using UnityEngine;
using EnemyAI.Utilites;

namespace EnemyAI 
{ 
    public class PatrolBehaviour : EnemyBehaviour
    {
        private const string ANIMATION_NAME = "Walking";

        private Vector3 _targetWaypoint;
        public PatrolBehaviour(EnemyBehaviourHandler enemyReference) : base(enemyReference)
        {
            _animationName = ANIMATION_NAME;
            _canBeUpdated = true;
        }
    
        public override void Enter()
        {
            _animationName = ANIMATION_NAME;
            _targetWaypoint = GetNextWaypoint();
            MoveToWaypoint();

            _enemyReference.EnemyMover.StartMoving();
        }
    
        public override void Update()
        {
            if (!EnemyBehaviourUtilities.CheckIsDestinationReached(_enemyReference, _targetWaypoint))
                return;
                
            _targetWaypoint = GetNextWaypoint();
            MoveToWaypoint();
        }
    
        public override void Exit()
        {
            _enemyReference.EnemyMover.StopMoving();
        }

        private Vector3 GetNextWaypoint()
        {
            return _enemyReference.WaypointCointainer.Waypoints[Random.Range(0, _enemyReference.WaypointCointainer.Waypoints.Length)].transform.position;
        }
        private void MoveToWaypoint()
        {
            _enemyReference.EnemyMover.Agent.SetDestination(_targetWaypoint);
        }
    }
}