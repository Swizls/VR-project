using System;
using System.Collections;
using System.Collections.Generic;
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

        _playerReference.GetComponent<Health>().Died += Restart;
    }

    private void Restart()
    {
        _playerReference.GetComponent<Health>().Died -= Restart;
        _sceneHandler.Restart();
    }
}
