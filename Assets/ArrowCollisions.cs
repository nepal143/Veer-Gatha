using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowCollisions : MonoBehaviour
{
    public string nextSceneName = "NextScene"; // Set your scene name here

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("die", true);
            }

            // Wait for 2 seconds, then change the scene
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
