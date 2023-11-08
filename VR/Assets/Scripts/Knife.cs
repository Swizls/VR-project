using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Knife : Weapon
{
    private void Attack(HitReaction hitable)
    {
        hitable.HitReaction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HitReaction hitable))
            Attack(hitable);
    }
}
