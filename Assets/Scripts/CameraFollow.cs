using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object to follow (assign in Inspector)
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Camera offset
    public float followSpeed = 5f; // How smoothly the camera follows
    public float rotationSpeed = 5f; // How smoothly the camera rotates

    void LateUpdate()
    {
        if (target == null) return; // Exit if no target assigned

        // Smoothly move the camera to the target's position + offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate to look at the target
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
