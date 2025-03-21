using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerOnTrigger : MonoBehaviour
{
    public string sceneName; // Set this in the Inspector to the name of the scene you want to load

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello") ;
        if (other.CompareTag("Player")) // Ensure your player has the tag "Player"
        {
            Debug.Log("hello11") ;
            SceneManager.LoadScene(sceneName);
        }
    }
}
