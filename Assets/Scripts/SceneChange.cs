using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Call this method to change scenes
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Example: Change scene by index
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the application (only works in a built game)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); // Useful for testing in the Unity Editor
    }
}