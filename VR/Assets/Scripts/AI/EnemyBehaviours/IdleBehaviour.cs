using UnityEngine;
using EnemyAI.Utilites;

namespace EnemyAI
{
    public class IdleBehaviour : EnemyBehaviour
    {
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
            Debug.Log("Enter " + _enemyReference);
            _enemyReference.EnemyMover.Agent.SetDestination(_startPosition);
        }

        public override void Update()
        {
            Debug.Log("Update " + _enemyReference);

            if (!EnemyBehaviourUtilities.CheckIsDestinationReached(_enemyReference, _startPosition))
                return;

            _canBeUpdated = false;
            _enemyReference.transform.rotation = _startRotation;

        }

        public override void Exit()
        {
            _canBeUpdated = true;
        }
    }
}
