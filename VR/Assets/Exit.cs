using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private ColliderTrigger _trigger;
    [SerializeField] private SceneHandler _sceneHandler;

    private void Start()
    {
        if (_trigger == null)
        {
            _trigger = GetComponentInChildren<ColliderTrigger>();
            _trigger.Triggered += OnExitActivation;
        }

        if(_sceneHandler == null)
        {
            _sceneHandler = FindObjectOfType<SceneHandler>();
        }

    }

    private void OnExitActivation(GameObject obj)
    {
        if (_sceneHandler == null)
            return;

        _sceneHandler.LoadMenu();
    }
}
