using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementTracker : MonoBehaviour, IMovable
{
    private const float SPEED_THRESHOLD = 0.5f;

    [SerializeField] private CharacterController _characterController;

    public event Action<bool> MovingStateChanged;

    private bool _isMoving = false;

    private void Start()
    {
        if( _characterController != null )
            _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (CheckIsPlayerMoving() && !_isMoving)
        {
            _isMoving = true;
            MovingStateChanged?.Invoke(_isMoving);
        }
        else if (!CheckIsPlayerMoving() && _isMoving)
        {
            _isMoving = false;
            MovingStateChanged?.Invoke(_isMoving);
        }
    }
    private bool CheckIsPlayerMoving()
    {
        return _characterController.velocity.magnitude == SPEED_THRESHOLD;
    }
}
