using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowShoot : MonoBehaviour
{
    public GameObject arrowPrefab; // Arrow prefab to be instantiated
    public Transform firePoint; // The firing point
    public float maxShootForce = 25f; // Max force when fully held
    public float maxHoldDuration = 3f; // Max duration for holding the button
    public Button shootButton; // UI Button

    private float holdTime = 0f; // Track how long the button is held
    private bool isHolding = false; // Whether the button is currently being held down

    void Start()
    {
        // Ensure the shoot button has an EventTrigger component
        EventTrigger trigger = shootButton.gameObject.GetComponent<EventTrigger>() ?? shootButton.gameObject.AddComponent<EventTrigger>();

        // Add Pointer Down event
        AddEventTrigger(trigger, EventTriggerType.PointerDown, (eventData) => StartHolding());

        // Add Pointer Up event
        AddEventTrigger(trigger, EventTriggerType.PointerUp, (eventData) => ReleaseAndShoot());
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0f, maxHoldDuration);
        }
    }

    void StartHolding()
    {
        isHolding = true;
        holdTime = 0f;
        Debug.Log("Button Pressed: Holding started.");
    }

    void ReleaseAndShoot()
    {
        if (isHolding)
        {
            ShootArrow();
            isHolding = false;
            Debug.Log("Button Released: Shooting arrow.");
        }
    }

    void ShootArrow()
    {
        float currentForce = Mathf.Lerp(0f, maxShootForce, holdTime / maxHoldDuration);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * currentForce, ForceMode.Impulse);
        }

        Debug.Log("Hold Time: " + holdTime + " seconds");
        Debug.Log("Arrow Force: " + currentForce);
    }

    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action(eventData));
        trigger.triggers.Add(entry);
    }
}
