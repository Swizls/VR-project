using Game.Enemies.AI;
using UnityEngine;
using System;

namespace Game.Enemies 
{
    [RequireComponent(typeof(EnemyBehaviourHandler))]
    public class EnemyAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private EnemyBehaviourHandler _enemyBehiviourHandler;
        private EnemyBehaviour _currentBehaviour;

        private int _upperBodyLayerIndex;

        private void Start()
        {
            _enemyBehiviourHandler = GetComponent<EnemyBehaviourHandler>();

            if (_animator == null)
                throw new NullReferenceException();

            _enemyBehiviourHandler.BehaviourChanged += SetAnimation;
            _upperBodyLayerIndex = _animator.GetLayerIndex("UpperBody");
        }

        private void Update()
        {
            float speed = _enemyBehiviourHandler.EnemyMover.Agent.velocity.magnitude;
            _animator.SetFloat("MovementSpeed", speed);
        }

        private void SetAnimation(EnemyBehaviour behaviour)
        {
            if (_currentBehaviour != null)
                _currentBehaviour.BehaviourEnded -= SetAnimation;

            _currentBehaviour = behaviour;

            if (_currentBehaviour.GetType() == typeof(PatrolBehaviour))
                _animator.SetLayerWeight(_upperBodyLayerIndex, 0f);
            else
                _animator.SetLayerWeight(_upperBodyLayerIndex, 1f);

            _currentBehaviour.BehaviourEnded += SetAnimation;
            _animator.Play(behaviour.AnimationName);
        }
    }
}