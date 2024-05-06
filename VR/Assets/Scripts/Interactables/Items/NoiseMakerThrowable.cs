using Game.Enemies.AI;
using UnityEngine;

public class NoiseMakerThrowable : NoiseMaker
{
    private const float MIN_VELOCITY_TO_MAKE_NOISE = 1f;

    [SerializeField] private bool _canBeDestroyedOnLand;

    public override void MakeNoise()
    {
        base.MakeNoise();
        if (_canBeDestroyedOnLand)
        {
            _audio.clip = _noiseSound;
            _audio.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > MIN_VELOCITY_TO_MAKE_NOISE 
        || Mathf.Abs(_rigidbody.velocity.y) > MIN_VELOCITY_TO_MAKE_NOISE 
        || Mathf.Abs(_rigidbody.velocity.z) > MIN_VELOCITY_TO_MAKE_NOISE) 
            MakeNoise();
    }
}
