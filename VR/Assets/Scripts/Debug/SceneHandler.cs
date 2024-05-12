using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Scene _scene;
    [SerializeField] private int _mainSceneIndex;
    [SerializeField] private int _mainMenuIndex;

    public void Restart()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(_mainMenuIndex);
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }
}
