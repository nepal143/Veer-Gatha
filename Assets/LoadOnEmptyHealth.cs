using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnEmptyHealth : MonoBehaviour
{
    public Image healthBar; // Assign the health bar UI Image in Inspector
    public string sceneToLoad; // Set the scene name in Inspector

    void Update()
    {
        if (healthBar.fillAmount <= 0)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in LoadSceneOnEmptyHealth script.");
        }
    }
}
