using System;
using UnityEngine;

public class Health : MonoBehaviour, IHitReaction
{
    [SerializeField][Range(0, 100)] private int _value;

    public event Action Died;

    public int Value => _value;

    public void HitReaction(int damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        if(damage < 0) 
            throw new System.ArgumentException();

        _value -= damage;

        if (_value <= 0)
            Died?.Invoke();
    }
    public void Heal(int healValue)
    {
        if(healValue < 0)
            throw new System.ArgumentException();

        _value += healValue;
    }
}
