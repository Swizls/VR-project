using Game.Enemies.AI;
using UnityEngine;

namespace Game.Enemies 
{
    [RequireComponent(typeof(EnemyBehaviourHandler))]
    public class EnemyAnimationSwitcher : MonoBehaviour
    {
        private EnemyBehaviourHandler _enemyBehiviourHandler;
        private EnemyBehaviour _currentBehaviour;

        private Animator _animator;

        private int _upperBodyLayerIndex;

        private void Start()
        {
            _enemyBehiviourHandler = GetComponent<EnemyBehaviourHandler>();

            _animator = GetComponentInChildren<Animator>();

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