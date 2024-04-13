using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IHitReaction
{
    public void HitReaction(int dagame)
    {
        Destroy(gameObject);
    }
}
