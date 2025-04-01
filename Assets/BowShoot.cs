using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowShoot : MonoBehaviour
{
    public GameObject arrowPrefab; // Arrow prefab
    public Transform firePoint; // Arrow firing position
    public float maxShootForce = 25f; // Maximum shoot force
    public float maxHoldDuration = 3f; // Maximum time to hold

    public Button shootButton; // UI Shoot Button
    public Image powerFillImage; // UI Image that fills & changes color

    private float holdTime = 0f; // Track hold duration
    private bool isHolding = false; // Whether the button is held

    void Start()
    {
        // Ensure the shoot button has an EventTrigger component
        EventTrigger trigger = shootButton.gameObject.GetComponent<EventTrigger>() ?? shootButton.gameObject.AddComponent<EventTrigger>();

        // Add Pointer Down event
        AddEventTrigger(trigger, EventTriggerType.PointerDown, (eventData) => StartHolding());

        // Add Pointer Up event
        AddEventTrigger(trigger, EventTriggerType.PointerUp, (eventData) => ReleaseAndShoot());

        // Initialize UI Image
        if (powerFillImage != null)
        {
            powerFillImage.fillAmount = 0f;
            powerFillImage.color = Color.green; // Start as green
        }
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0f, maxHoldDuration);

            // Update UI fill amount (0 to 1)
            if (powerFillImage != null)
            {
                float fillAmount = holdTime / maxHoldDuration;
                powerFillImage.fillAmount = fillAmount;

                // Lerp color from Green (low) to Red (max)
                powerFillImage.color = Color.Lerp(Color.green, Color.red, fillAmount);
            }
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

            // Reset UI fill
            if (powerFillImage != null)
            {
                powerFillImage.fillAmount = 0f;
                powerFillImage.color = Color.green;
            }

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

        Debug.Log($"Hold Time: {holdTime} seconds");
        Debug.Log($"Arrow Force: {currentForce}");
    }

    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action(eventData));
        trigger.triggers.Add(entry);
    }
}
