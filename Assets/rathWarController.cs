using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RathWar : MonoBehaviour
{
    public Button moveLeftButton;
    public Button moveRightButton;
    public float moveSpeed = 5f;
    public float moveLimit = 200f;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;

        AddEventTrigger(moveLeftButton.gameObject, EventTriggerType.PointerDown, () => isMovingLeft = true);
        AddEventTrigger(moveLeftButton.gameObject, EventTriggerType.PointerUp, () => isMovingLeft = false);

        AddEventTrigger(moveRightButton.gameObject, EventTriggerType.PointerDown, () => isMovingRight = true);
        AddEventTrigger(moveRightButton.gameObject, EventTriggerType.PointerUp, () => isMovingRight = false);
    }

    void Update()
    {
        float moveDelta = moveSpeed * Time.deltaTime;
        if (isMovingLeft && transform.position.x > initialX - moveLimit)
        {
            transform.Translate(Vector3.left * moveDelta);
        }
        if (isMovingRight && transform.position.x < initialX + moveLimit)
        {
            transform.Translate(Vector3.right * moveDelta);
        }

        // Disable buttons when the limit is reached
        moveLeftButton.interactable = transform.position.x > initialX - moveLimit;
        moveRightButton.interactable = transform.position.x < initialX + moveLimit;
    }

    void AddEventTrigger(GameObject obj, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action());
        trigger.triggers.Add(entry);
    }
} 