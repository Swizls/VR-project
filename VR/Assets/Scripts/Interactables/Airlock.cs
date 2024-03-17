using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Airlock : MonoBehaviour
{
    private const string OPENING_ANIMATION = "Opened";
    private const string CLOSING_ANIMATION = "Closed";

    [SerializeField] private float _timeToHold;

    private bool _isOpen = false;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        _isOpen = true;
        _animator.SetBool("isOpen", _isOpen);
        _audioSource.Play();
        StartCoroutine(HoldOpen());
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
