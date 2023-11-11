using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField][Range(0,360)] private float _angle;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    [SerializeField] private bool _isPlayerSpotted;

    public Action<Vector3> PlayerHasBeenSpotted;
    public Action<Vector3> PlayerHasBeenLost;

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
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                _isPlayerSpotted = Physics.Raycast(transform.position + Vector3.up, directionToTarget + Vector3.up, distanceToTarget, _obstructionMask);
                PlayerHasBeenSpotted?.Invoke(target.position);
            }
            else
            {
                _isPlayerSpotted = false;
                PlayerHasBeenLost?.Invoke(target.position);
            }
        }
        else if (_isPlayerSpotted)
        {
            _isPlayerSpotted = false;
        }
    }
}
