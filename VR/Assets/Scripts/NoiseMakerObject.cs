using EnemyAI;
using UnityEngine;

public class NoiseMakerObject : MonoBehaviour
{
    private const float MIN_VELOCITY_TO_MAKE_NOISE = 1f;

    [SerializeField] private LayerMask _enemiesMask;

    [Space]
    [SerializeField] private AudioClip _destroySound;

    [Space]
    [SerializeField] private int _noiseRadius;
    [SerializeField] private bool _canBeDestroyedOnLand;

    private AudioSource _audio;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void MakeNoise()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _noiseRadius, _enemiesMask);

        if(enemiesInRange.Length > 0)
        {
            foreach(Collider enemy in enemiesInRange)
            {
                enemy.GetComponent<EnemyBehaviourHandler>().Agent.SetDestination(transform.position);
            }
        }
        if (_canBeDestroyedOnLand)
        {
            _audio.clip = _destroySound;
            _audio.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(_rigidbody.velocity);
        if (Mathf.Abs(_rigidbody.velocity.x) > MIN_VELOCITY_TO_MAKE_NOISE 
        || Mathf.Abs(_rigidbody.velocity.y) > MIN_VELOCITY_TO_MAKE_NOISE 
        || Mathf.Abs(_rigidbody.velocity.z) > MIN_VELOCITY_TO_MAKE_NOISE) 
            MakeNoise();
    }
}
