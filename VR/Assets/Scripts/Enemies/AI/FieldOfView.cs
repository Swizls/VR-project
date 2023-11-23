using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI 
{ 
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField][Range(0,360)] private float _angle;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        private bool _isPlayerSpotted;
        private bool _isEnemyChasingPlayer = false;

        public Action PlayerHasBeenSpotted;
        public Action PlayerHasBeenLost;

        public float Radius => _radius;
        public float Angle => _angle;

        private void FixedUpdate()
        {
            _isPlayerSpotted = FieldOfViewCheck();

            if (_isPlayerSpotted && !_isEnemyChasingPlayer)
            {
                _isEnemyChasingPlayer = true;
                PlayerHasBeenSpotted?.Invoke();
            }
            else if(!_isPlayerSpotted && _isEnemyChasingPlayer)
            {
                _isEnemyChasingPlayer = false;
                PlayerHasBeenLost?.Invoke();
            }
        }

        private bool FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            if (rangeChecks.Length != 0)
            {
                Vector3 targetPosition = rangeChecks[0].transform.position;
                Vector3 directionToTarget = (targetPosition - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                    return !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask);
                }
            }
            return false;
        }
    }
}