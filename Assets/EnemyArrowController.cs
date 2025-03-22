using UnityEngine;

public class EnemyArrowController : MonoBehaviour
{
    public float arrowForce = 20f;
    public float destroyTime = 2f;
    private Vector3 targetPosition;

    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y += 0.3f; // Adjust for projectile arc
            rb.AddForce(direction * arrowForce, ForceMode.Impulse);
        }

        Destroy(gameObject, destroyTime);
    }
} 
