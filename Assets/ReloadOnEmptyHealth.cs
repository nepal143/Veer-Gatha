using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadSceneOnEmptyHealth : MonoBehaviour
{
    public Image healthBar; // Assign the health bar UI Image in Inspector

    void Update()
    {
        if (healthBar.fillAmount <= 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
