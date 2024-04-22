using UnityEngine;

namespace CustomDebug
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private int _damage;

        public void GiveDamage() => _target.TakeDamage(_damage);
    }
}
