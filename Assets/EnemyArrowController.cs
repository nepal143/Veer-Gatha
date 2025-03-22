using UnityEngine;

public class EnemyArrowController : MonoBehaviour
{
    public GameObject arrowPrefab; // Arrow prefab to instantiate
    public float arrowForce = 20f;
    public float destroyTime = 2f;
    private Vector3 targetPosition;

    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y += 0.3f; // Adjust for projectile arc
            rb.AddForce(direction * arrowForce, ForceMode.Impulse);
        }

        Destroy(arrow, destroyTime);
    }
} 
