using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Scene _scene;
    [SerializeField] private int _mainSceneIndex;

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }
}
