using UnityEngine;

namespace EnemyAI 
{ 
    public class PatrolBehaviour : EnemyBehaviour
    {
        private const string ANIMATION_NAME = "Walking";

        private Vector3 _startWaypoint;
        public PatrolBehaviour(EnemyBehaviourHandler enemyReference, Vector3 startWaypoint) : base(enemyReference)
        {
            _animationName = ANIMATION_NAME;
            _canBeUpdated = false;
            _startWaypoint = startWaypoint;
        }
    
        public override void Enter()
        {
            if(_startWaypoint == null)
                throw new System.ArgumentException();
    
            _enemyReference.EnemyMover.Agent.SetDestination(_startWaypoint);
            _enemyReference.EnemyMover.StartMoving();
        }
    
        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    
        public override void Exit()
        {
            _enemyReference.EnemyMover.StopMoving();
        }
    }
}