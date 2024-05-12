using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private SceneHandler _sceneHandler;
    private GameObject _playerReference;

    private void Start()
    {
        if(_sceneHandler == null)
            _sceneHandler = FindAnyObjectByType<SceneHandler>();

        if(_playerReference == null)
            _playerReference = FindAnyObjectByType<CharacterController>().gameObject;

        _playerReference.GetComponent<Health>().Died += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _playerReference.GetComponent<Health>().Died -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _playerReference.GetComponent<Health>().Died -= OnPlayerDeath;
        _sceneHandler.Restart();
    }
}
