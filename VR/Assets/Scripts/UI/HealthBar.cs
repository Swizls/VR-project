using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private const float ANIMATION_SPEED = 2f;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private Transform _healthBar;

    private void OnEnable() => _playerHealth.HealthChanged += SetBarValue;

    private void OnDisable() => _playerHealth.HealthChanged -= SetBarValue;

    private void SetBarValue(int value) => StartCoroutine(PlayBarAnimation((float)value / 100));

    private IEnumerator PlayBarAnimation(float value)
    {
        while(value < _healthBar.localScale.x)
        {
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, new Vector3(value, 1, 1), Time.deltaTime * ANIMATION_SPEED);
            yield return null;
        }
    }
}
