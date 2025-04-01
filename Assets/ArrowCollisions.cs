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

            // Move enemy's Y position to -0.7
            Vector3 enemyPosition = collision.gameObject.transform.position;
            // collision.gameObject.transform.position = new Vector3(enemyPosition.x, -0.7f, enemyPosition.z);

            // Turn off the Main Camera
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
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
