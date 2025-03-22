using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerMaxHealth = 100;
    public int enemyMaxHealth = 100;
    public int arrowDamage = 10;
    public Button fireButton;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Transform enemyTarget;
    public float fireCooldown = 2f;
    public float arrowForce = 20f;

    private int playerCurrentHealth;
    private int enemyCurrentHealth;
    private float lastFireTime;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        enemyCurrentHealth = enemyMaxHealth;
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

        // Destroy the arrow after 2 seconds
        Destroy(arrow, 2f);

        // Delay the damage to enemy by 1.5 seconds
        Invoke(nameof(DealEnemyDamage), 1.5f);

        lastFireTime = Time.time;
    }

    void DealEnemyDamage()
    {
        TakeDamage(false, arrowDamage);
    }

    public void TakeDamage(bool isPlayer, int damage)
    {
        if (isPlayer)
        {
            playerCurrentHealth -= damage;
            playerCurrentHealth = Mathf.Max(playerCurrentHealth, 0);
            Debug.Log($"Player Health: {playerCurrentHealth}");

            if (playerCurrentHealth <= 0)
            {
                Debug.Log("Player has died.");
            }
        }
        else
        {
            enemyCurrentHealth -= damage;
            enemyCurrentHealth = Mathf.Max(enemyCurrentHealth, 0);
            Debug.Log($"Enemy Health: {enemyCurrentHealth}");

            if (enemyCurrentHealth <= 0)
            {
                Debug.Log("Enemy has died.");
            }
        }
    }
}
