using Game.Enemies.AI;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;
using System.Collections;

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

        private float _switchingSpeed = 5f;

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
                StopAllCoroutines();
                StartCoroutine(StartAnimationEaseSwitching(1f));
                _aimPoint.enabled = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(StartAnimationEaseSwitching(0f));
                _aimPoint.enabled = false;
            }

            _currentBehaviour.BehaviourEnded += SetAnimation;
            _animator.Play(behaviour.AnimationName);
        }
        private IEnumerator StartAnimationEaseSwitching(float targetValue)
        {
            while (_animator.GetLayerWeight(_upperBodyLayerIndex) != targetValue)
            {
                _animator.SetLayerWeight(_upperBodyLayerIndex, Mathf.Lerp(_animator.GetLayerWeight(_upperBodyLayerIndex), targetValue, Time.deltaTime * _switchingSpeed));
                _rig.weight = Mathf.Lerp(_rig.weight, targetValue, Time.deltaTime * _switchingSpeed);
                yield return null;
            }
        }
    }
}
