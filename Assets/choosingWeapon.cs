using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button targetButton; // Button to trigger actions
    public Color newBackgroundColor; // Background color for the button
    public GameObject panelToDisable; // Panel to disable
    public GameObject panelToEnable; // Panel to enable
    public Camera mainCamera; // Assign the main camera

    public bool isShakeEnabled = true; // ✅ Toggle this in the Inspector

    private Image buttonImage;
    private Vector3 originalCameraPos;

    void Start()
    {
        if (targetButton != null)
        {
            buttonImage = targetButton.GetComponent<Image>();
            targetButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("❌ No Button assigned!");
        }

        if (mainCamera != null)
        {
            originalCameraPos = mainCamera.transform.position;
        }
    }

    void OnButtonClick()
    {
        if (buttonImage != null)
        {
            buttonImage.color = newBackgroundColor; // Change button color
        }

        if (isShakeEnabled) // 🔥 Uses the bool value you set in the Inspector
        {
            StartCoroutine(ShakeCamera(0.2f, 0.2f)); // Shake effect
        }

        StartCoroutine(SwitchPanelsAfterDelay(1f)); // Delay panel switch
    }

    IEnumerator SwitchPanelsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (panelToDisable != null) panelToDisable.SetActive(false);
        if (panelToEnable != null) panelToEnable.SetActive(true);
    }

    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude);
            float offsetY = Random.Range(-magnitude, magnitude);

            if (mainCamera != null)
            {
                mainCamera.transform.position = originalCameraPos + new Vector3(offsetX, offsetY, 0);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPos; // Reset camera position
        }
    }
}
