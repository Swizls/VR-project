using Game.Enemies.AI.Utilites;
using UnityEngine;

namespace Game.Enemies.AI
{
    public class SearchBehaviour : EnemyBehaviour
    {
        private const string IDLE_ANIMATION_NAME = "Idle";
        private const string WALK_ANIMATION_NAME = "Walking";
        private const float DEFAULT_TIMER_TIME = 10f;

        private bool _destinationIsReached = false;
        private float _timer = 10f;

        public SearchBehaviour(EnemyBehaviourHandler enemyReference) : base(enemyReference)
        {
            _animationName = WALK_ANIMATION_NAME;
            _canBeUpdated = true;
        }

        public override void Enter()
        {
            _enemyReference.EnemyMover.Agent.SetDestination(_enemyReference.PositionToSearch);
            _enemyReference.EnemyMover.StartMoving();
        }

        public override void Update()
        {
            if (EnemyBehaviourUtilities.CheckIsDestinationReached(_enemyReference, _enemyReference.PositionToSearch) && !_destinationIsReached)
            {
                _destinationIsReached = true;
                return;
            }

            _animationName = IDLE_ANIMATION_NAME;

            _timer -= Time.deltaTime;
            if(_timer < 0) 
            {
                BehaviourEnded?.Invoke(this);
            }
        }

        public override void Exit()
        {
            _timer = DEFAULT_TIMER_TIME;
            _enemyReference.EnemyMover.StopMoving();
        }
    }
}