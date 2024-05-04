using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Airlock : MonoBehaviour
{
    [SerializeField] private float _timeToHold;
    [SerializeField] private bool _isLocked;

    private bool _isOpen = false;

    private NavMeshObstacle _navMeshObstacle;
    private Animator _animator;
    private AudioSource _audioSource;

    public event Action LockValueChanged;

    public bool IsLocked => _isLocked;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        _navMeshObstacle.enabled = _isLocked;
    }

    public void Open()
    {
        if (_isOpen || _isLocked)
            return;

        _isOpen = true;
        _animator.SetBool("isOpen", _isOpen);
        _audioSource.Play();
        StartCoroutine(HoldOpen());
    }

    public void ToggleLock()
    {
        _isLocked = !_isLocked;
        _navMeshObstacle.enabled = _isLocked;
        LockValueChanged?.Invoke();
    }

    private IEnumerator HoldOpen()
    {
        float time = _timeToHold;

        while (time > 0) 
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0);
        }

        _isOpen = false;
        _animator.SetBool("isOpen", _isOpen);
    }
}
