using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    private CameraFollow cameraFollow; // Reference to your CameraFollow script
    private Transform player; // Player transform will be found by tag

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag

        if (cameraFollow != null)
        {
            cameraFollow.target = transform;    
            StartCoroutine(ResetCameraAfterDelay(3f));
        }
    }

    private IEnumerator ResetCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (cameraFollow != null)
        {
            cameraFollow.target = player; // Switch back to the player
        }

        Destroy(gameObject);
    }
}
