using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private Scene _scene;

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter(Collider collider)
    {
        Restart();
    }
}
