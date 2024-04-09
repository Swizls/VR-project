using EnemyAI;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    [SerializeField] protected LayerMask _enemiesMask;

    [Space]
    [SerializeField] protected AudioClip _noiseSound;

    [Space]
    [SerializeField] protected int _noiseRadius;

    protected AudioSource _audio;
    protected Rigidbody _rigidbody;

    public int NoiseRadius => _noiseRadius;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void MakeNoise()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _noiseRadius, _enemiesMask);

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider enemy in enemiesInRange)
            {
                enemy.GetComponent<EnemyBehaviourHandler>().ReactionOnNoise(transform.position);
            }
        }
    }
}
