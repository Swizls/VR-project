using UnityEngine;

public class Knife : MonoBehaviour
{
    private void Attack(IHitReaction hitable)
    {
        hitable.HitReaction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHitReaction hitable))
            Attack(hitable);
    }
}
