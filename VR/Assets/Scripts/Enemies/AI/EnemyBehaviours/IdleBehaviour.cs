using UnityEngine;
using Game.Enemies.AI.Utilites;

namespace Game.Enemies.AI
{
    public class IdleBehaviour : EnemyBehaviour
    {
        private const string WALK_ANIMATION_NAME = "Walking";
        private const string IDLE_ANIMATION_NAME = "Idle";

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        public IdleBehaviour(EnemyBehaviourHandler enemyReference, Transform startTransform) : base(enemyReference)
        {
            _canBeUpdated = true;
            _startPosition = startTransform.position;
            _startRotation = startTransform.rotation;
        }

        public override void Enter()
        {
            if (EnemyBehaviourUtilities.CheckIsDestinationReached(_enemyReference, _startPosition))
            {
                _animationName = IDLE_ANIMATION_NAME;
            }
            else
            {
                _enemyReference.EnemyMover.Agent.SetDestination(_startPosition);
                _enemyReference.EnemyMover.StartMoving();
                _animationName = WALK_ANIMATION_NAME;
            }
        }

        public override void Update()
        {
            if (!EnemyBehaviourUtilities.CheckIsDestinationReached(_enemyReference, _startPosition))
                return;

            _canBeUpdated = false;
            _enemyReference.transform.rotation = _startRotation;
            _animationName = IDLE_ANIMATION_NAME;
            BehaviourEnded.Invoke(this);
        }

        public override void Exit()
        {
            _canBeUpdated = true;
            _enemyReference.EnemyMover.StopMoving();
        }
    }
}
