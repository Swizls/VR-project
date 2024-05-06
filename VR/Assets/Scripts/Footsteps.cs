using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _footstepSounds;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private IMovable _targetMover;

    private void Start()
    {
        _targetMover = GetComponent<IMovable>();

        _targetMover.MovingStateChanged += PlayFootstepsSounds;
    }

    private void OnDisable()
    {
        _targetMover.MovingStateChanged -= PlayFootstepsSounds;
    }

    private void PlayFootstepsSounds(bool movingState)
    {
        if (movingState)
        {
            if (_audioSource.isPlaying)
                return;

            _audioSource.clip = _footstepSounds[Random.Range(0, _footstepSounds.Count - 1)];
            _audioSource.Play();
            return;
        }
        _audioSource.Stop();
    }

}
