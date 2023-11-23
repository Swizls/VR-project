using EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviourHandler))]
public class EnemyAnimationSwitcher : MonoBehaviour
{
    private EnemyBehaviourHandler _enemyBehiviourHandler;
    private EnemyBehaviour _currentBehaviour;

    private Animator _animator;

    private void Start()
    {
        _enemyBehiviourHandler = GetComponent<EnemyBehaviourHandler>();

        _animator = GetComponentInChildren<Animator>();

        _enemyBehiviourHandler.BehaviourChanged += SetAnimation;
    }

    private void SetAnimation(EnemyBehaviour behaviour)
    {
        if(_currentBehaviour != null)
            _currentBehaviour.BehaviourEnded -= SetAnimation;

        _currentBehaviour = behaviour;

        _currentBehaviour.BehaviourEnded += SetAnimation;
        _animator.Play(behaviour.AnimationName);
    }
}
