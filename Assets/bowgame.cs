using UnityEngine;
using UnityEngine.UI;

public class DragToConnect : MonoBehaviour
{
    public RectTransform startPoint;  // Assign the UI Image at the starting position (fixed point on bow)
    public RectTransform endPoint;    // Assign the UI Image at the endpoint (where the string should connect)
    public RectTransform stringSprite; // Assign the UI Image that acts as the string

    private bool isDragging = false;

    void Start()
    {
        ResetString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = GetMousePosition();
            
            if (IsNear(mousePos, startPoint))
            {
                isDragging = true;
                stringSprite.gameObject.SetActive(true);
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = GetMousePosition();
                UpdateString(mousePos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = GetMousePosition();
                
                if (IsNear(mousePos, endPoint))
                {
                    UpdateString(endPoint.anchoredPosition); // Snap to endPoint
                }
                else
                {
                    ResetString(); // If released early, remove string
                }
                isDragging = false;
            }
        }
    }

    void UpdateString(Vector2 targetPos)
    {
        Vector2 direction = targetPos - startPoint.anchoredPosition;
        float distance = direction.magnitude;

        // Set width to match distance
        stringSprite.sizeDelta = new Vector2(distance, stringSprite.sizeDelta.y);

        // Anchor one end at startPoint
        stringSprite.anchoredPosition = startPoint.anchoredPosition;

        // Rotate string to match the angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        stringSprite.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ResetString()
    {
        stringSprite.sizeDelta = new Vector2(0, stringSprite.sizeDelta.y); // Hide the string
        stringSprite.gameObject.SetActive(false);
    }

    bool IsNear(Vector2 position, RectTransform target)
    {
        return Vector2.Distance(position, target.anchoredPosition) < 30f; // Adjust as needed
    }

    Vector2 GetMousePosition()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(startPoint.parent as RectTransform, Input.mousePosition, null, out mousePos);
        return mousePos;
    }
}
