using Game.Enemies.AI;
using UnityEngine;

namespace Game.Enemies 
{
    [RequireComponent(typeof(RagdollHandler))]
    [RequireComponent(typeof(AudioSource))]
    public class EnemyDeathHandler : DeathHandler, IHitReaction
    {
        private RagdollHandler _ragdollHandler;
        private AudioSource _enemyFootstepsAudio;
        private Weapon _enemyWeapon;

        private void Start()
        {
            _ragdollHandler = GetComponent<RagdollHandler>();
            _enemyFootstepsAudio = GetComponent<AudioSource>();
            _enemyWeapon = GetComponent<EnemyBehaviourHandler>().Weapon;
        }

        public override void Die()
        {
            _enemyFootstepsAudio.Stop();
            _ragdollHandler.ActivateRagdoll();
            _enemyWeapon.GetComponent<Rigidbody>().isKinematic = false;
        }

        public void HitReaction(int dagame = 0) => Die();
    }
}
