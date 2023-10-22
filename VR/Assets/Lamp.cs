using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, HitReaction
{
    public void HitReaction()
    {
        Destroy(gameObject);
    }
}
