using UnityEngine;
using UnityEngine.UI;

public class BowShoot : MonoBehaviour
{
    public GameObject arrowPrefab; // Assign your arrow prefab here
    public Transform firePoint; // Assign the firing point
    public float shootForce = 20f; // Hardcoded force
    public Button shootButton; // Assign the UI Button

    void Start()
    {
        shootButton.onClick.AddListener(ShootArrow);
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}
