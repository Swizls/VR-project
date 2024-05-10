using Game.Enemies.AI;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;

namespace Game.Enemies 
{
    [RequireComponent(typeof(EnemyBehaviourHandler))]
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rig _rig;
        [SerializeField] private AimPoint _aimPoint;

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

        private void OnDisable()
        {
            _enemyBehiviourHandler.BehaviourChanged -= SetAnimation;
        }

        private void SetAnimation(EnemyBehaviour behaviour)
        {
            if (_currentBehaviour != null)
                _currentBehaviour.BehaviourEnded -= SetAnimation;

            _currentBehaviour = behaviour;

            if (_currentBehaviour.GetType() == typeof(AttackBehaviour))
            {
                _rig.weight = 1f;
                _aimPoint.enabled = true;
                _animator.SetLayerWeight(_upperBodyLayerIndex, 1f);
            }
            else
            {
                _rig.weight = 0;
                _aimPoint.enabled = false;
                _animator.SetLayerWeight(_upperBodyLayerIndex, 0f);
            }

            _currentBehaviour.BehaviourEnded += SetAnimation;
            _animator.Play(behaviour.AnimationName);
        }
    }
}