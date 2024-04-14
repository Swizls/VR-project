using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}