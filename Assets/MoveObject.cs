using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of forward movement
    public float dragSpeed = 0.1f; // Speed of horizontal dragging
    public float xLimit = 20f;  // Limit for movement in X direction

    private bool isMoving = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // Detect mouse click or touch start
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            lastMousePosition = Input.mousePosition;
        }

        // Move forward in local Z while clicking/touching
        if (isMoving)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }

        // Handle horizontal drag movement
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            float moveX = delta.x * dragSpeed * Time.deltaTime; // Convert drag to movement
            Vector3 newPosition = transform.position + transform.right * moveX;

            // Clamp X movement within limits
            newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
            transform.position = newPosition;
        }

        // Stop movement when releasing click/touch
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }
}
