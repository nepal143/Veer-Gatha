using UnityEngine;
using UnityEngine.UI;

public class ArrowShooter : MonoBehaviour
{
    public Button fireButton;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Transform enemyTarget;
    public float fireCooldown = 2f;
    public float arrowForce = 20f;

    private float lastFireTime;

    void Start()
    {
        fireButton.onClick.AddListener(FireArrow);
    }

    void Update()
    {
        // Manage cooldown
        fireButton.interactable = Time.time >= lastFireTime + fireCooldown;
    }

    void FireArrow()
    {
        if (Time.time < lastFireTime + fireCooldown) return;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (enemyTarget.position - firePoint.position).normalized;
            direction.y += 0.3f; // Adjust for projectile arc
            rb.AddForce(direction * arrowForce, ForceMode.Impulse);
        }

        lastFireTime = Time.time;
    }
}
