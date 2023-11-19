using UnityEngine;

namespace EnemyAI 
{ 
    public class PatrolBehaviour : EnemyBehaviour
    {
        private Vector3 _startWaypoint;
        public PatrolBehaviour(EnemyBehaviourHandler enemyReference, Vector3 startWaypoint) : base(enemyReference)
        {
            _canBeUpdated = false;
            _startWaypoint = startWaypoint;
        }
    
        public override void Enter()
        {
            if(_startWaypoint == null)
                throw new System.ArgumentException();
    
            _enemyReference.EnemyMover.Agent.SetDestination(_startWaypoint);
        }
    
        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    
        public override void Exit()
        {
    
        }
    }
}