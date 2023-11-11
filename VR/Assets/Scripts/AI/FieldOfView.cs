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

        [SerializeField] private bool _isPlayerSpotted;

        public Action<Vector3> PlayerHasBeenSpotted;
        public Action PlayerHasBeenLost;

        public float Radius => _radius;
        public float Angle => _angle;
        public bool IsPlayerSpotted => _isPlayerSpotted;

        private void Start()
        {
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                _isPlayerSpotted = FieldOfViewCheck(out Vector3 playerPosition);

                if (_isPlayerSpotted)
                {
                    PlayerHasBeenSpotted?.Invoke(playerPosition);
                }
            }
        }

        private bool FieldOfViewCheck(out Vector3 targetPosition)
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            if (rangeChecks.Length != 0)
            {
                targetPosition = rangeChecks[0].transform.position;
                Vector3 directionToTarget = (targetPosition - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                    return !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask); ;
                }
            }
            targetPosition = Vector3.zero;
            return false;
        }
    }
}

