using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthStim : MonoBehaviour
{
    [SerializeField] private ColliderTrigger _trigger;
    [SerializeField] private int _healthPoints;

    [SerializeField] private AudioClip _healSound;

    private AudioSource _audio;

    private bool _isUsed = false;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        if(_trigger == null )
            throw new System.NullReferenceException();

        _trigger.Triggered += OnTriggerActivation;
    }

    private void OnDisable()
    {
        _trigger.Triggered -= OnTriggerActivation;
    }

    private void OnTriggerActivation(GameObject obj)
    {
        Health targetHealth = obj.GetComponent<Health>();
        targetHealth = targetHealth ? targetHealth : obj.GetComponentInParent<Health>();

        if (targetHealth == null)
            return;

        targetHealth.Heal(_healthPoints);
        _trigger.Deactivate();

        Debug.Log("Healed!");

        if (_healSound == null)
            return;

        _audio.clip = _healSound;
        _audio.Play();
    }
}
