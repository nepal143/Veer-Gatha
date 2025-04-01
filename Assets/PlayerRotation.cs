using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMouseX = Input.mousePosition.x;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        if (isDragging)
        {
            float deltaX = Input.mousePosition.x - lastMouseX;
            transform.Rotate(0f, deltaX * rotationSpeed * Time.deltaTime, 0f);
            lastMouseX = Input.mousePosition.x;
        }
    }
} 
