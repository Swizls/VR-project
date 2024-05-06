using UnityEngine;

namespace Game.Enemies 
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemySoundReaction : MonoBehaviour
    {
        [SerializeField] private AudioClip _playerSpotted;
        [SerializeField] private AudioClip _playerLost;
        [SerializeField] private AudioClip _playerKilled;

        private AudioSource _source;

        public AudioClip PlayerSpotted => _playerSpotted;
        public AudioClip PlayerLost => _playerLost;
        public AudioClip PlayerKilled => _playerKilled;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlaySoundReaction(AudioClip clip)
        {
            _source.clip = clip;
            _source.Stop();
            _source.Play();
        }
    }
}

