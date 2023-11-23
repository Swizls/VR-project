using EnemyAI;
using System;

namespace EnemyAI
{
    public abstract class EnemyBehaviour
    {
        private const string DEFAULT_ANIMATION_NAME = "Idle";

        protected EnemyBehaviourHandler _enemyReference;

        protected string _animationName = DEFAULT_ANIMATION_NAME;
        protected bool _canBeUpdated;

        public Action<EnemyBehaviour> BehaviourEnded;

        public string AnimationName => _animationName;
        public bool CanBeUpdated => _canBeUpdated;

        public EnemyBehaviour(EnemyBehaviourHandler enemyReference)
        {
            _enemyReference = enemyReference;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}