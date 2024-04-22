using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public event Action<bool> MovingStateChanged;

    public void StartMoving() => MovingStateChanged?.Invoke(true);
    public void StopMoving() => MovingStateChanged?.Invoke(false);

}