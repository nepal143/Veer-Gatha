using UnityEngine;

public class DragToConnect : MonoBehaviour
{
    public Transform startPoint;  // Fixed bow point
    public Transform endPoint;    // Where the string should attach
    public LineRenderer stringLine; // The bowstring

    public GameObject[] objectsToDisable; // Assign objects to disable after stringing the bow
    public GameObject objectToEnable;     // Assign the object to enable

    private bool isDragging = false;

    void Start()
    {
        ResetString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckClickOnObject(out Transform clickedObject) && clickedObject == startPoint)
            {
                isDragging = true;
                stringLine.enabled = true;
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPos = GetMouseWorldPosition();
                UpdateString(mouseWorldPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (CheckClickOnObject(out Transform clickedObject) && clickedObject == endPoint)
                {
                    UpdateString(endPoint.position); // Snap to endPoint
                    OnBowStringed(); // Perform enable/disable actions
                }
                else
                {
                    ResetString(); // If released early, remove string
                }
                isDragging = false;
            }
        }
    }

    void UpdateString(Vector3 targetPos)
    {
        stringLine.SetPosition(0, startPoint.position);
        stringLine.SetPosition(1, targetPos);
    }

    void ResetString()
    {
        stringLine.enabled = false;
    }

    void OnBowStringed()
    {
        // Disable objects
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        // Enable the specified object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }

    bool CheckClickOnObject(out Transform hitObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitObject = hit.transform;
            return true;
        }
        hitObject = null;
        return false;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(startPoint.position).z; // Maintain depth
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
