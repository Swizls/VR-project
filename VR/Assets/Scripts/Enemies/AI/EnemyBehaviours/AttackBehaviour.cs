using UnityEngine;

namespace EnemyAI 
{
    public class AttackBehaviour : EnemyBehaviour
    {
        private const string ATTACK_ANIMATION_NAME = "PistolShooting";
        private const float DEFAULT_TIMER_ATTACK = 1f;

        private float _timerAttack = DEFAULT_TIMER_ATTACK;

        private IHitReaction _target;

        public AttackBehaviour(EnemyBehaviourHandler enemyReference) : base(enemyReference) 
        {
            _animationName = ATTACK_ANIMATION_NAME;
            _canBeUpdated = true;
        }

        public override void Enter()
        {
            _target = _enemyReference.PlayerReference.GetComponent<Health>();
            _enemyReference.EnemyMover.Agent.isStopped = true;
        }

        public override void Update()
        {
            if (_timerAttack > 0f)
            {
                _timerAttack -= Time.deltaTime;
                return;
            }

            ImitateAttack();
            ResetTimer();
        }

        public override void Exit()
        {
            _enemyReference.EnemyMover.Agent.isStopped = false;
        }

        private void ImitateAttack()
        {
            if (_target == null)
                return;
            
            _target.HitReaction(_enemyReference.Weapon.Damage);


            _enemyReference.Weapon.GetComponent<GunEffectsController>().PlayShotEffects();
            //if(_enemyReference.Weapon is Gun)
            //{
            //    Gun enemyGun = (Gun) _enemyReference.Weapon;
            //    enemyGun.PlayShotEffects();
            //}
        }

        private void ResetTimer()
        {
            _timerAttack = DEFAULT_TIMER_ATTACK;
        }
    }
}