using UnityEngine;
using UnityEngine.UI;

public class AutoArrowShooter : MonoBehaviour
{
    public int playerMaxHealth = 100;
    public int enemyMaxHealth = 100;
    public int arrowDamage = 10;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Transform enemyTarget;
    public float fireCooldown = 4f;
    public float arrowForce = 20f;
    public Image enemyHealthBar;
    public Image playerHealthBar; // Added player health bar

    private int playerCurrentHealth;
    private int enemyCurrentHealth;
    private float lastFireTime;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        enemyCurrentHealth = enemyMaxHealth;
        InvokeRepeating(nameof(FireArrow), fireCooldown, fireCooldown);
        UpdateEnemyHealthBar();
        UpdatePlayerHealthBar(); // Update player health bar on start
    }

    void FireArrow()
    {
        if (enemyTarget == null) return;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (enemyTarget.position - firePoint.position).normalized;
            direction.y += 0.3f; // Adjust for projectile arc
            rb.AddForce(direction * arrowForce, ForceMode.Impulse);
        }

        Arrow arrowScript = arrow.AddComponent<Arrow>();
        arrowScript.damage = arrowDamage;
        arrowScript.shooter = this;

        // Destroy the arrow after 2 seconds
        Destroy(arrow, 2f);
    }

    public void TakeDamage(bool isPlayer, int damage)
    {
        if (isPlayer)
        {
            playerCurrentHealth -= damage;
            playerCurrentHealth = Mathf.Max(playerCurrentHealth, 0);
            Debug.Log($"Player Health: {playerCurrentHealth}");
            UpdatePlayerHealthBar(); // Update player health bar when damaged

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
            UpdateEnemyHealthBar();

            if (enemyCurrentHealth <= 0)
            {
                Debug.Log("Enemy has died.");
            }
        }
    }

    void UpdateEnemyHealthBar()
    {
        if (enemyHealthBar != null)
        {
            enemyHealthBar.fillAmount = (float)enemyCurrentHealth / enemyMaxHealth;
        }
    }

    void UpdatePlayerHealthBar() // Added function to update player health bar
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.fillAmount = (float)playerCurrentHealth / playerMaxHealth;
        }
    }
}

public class Arrow : MonoBehaviour
{
    public int damage;
    public AutoArrowShooter shooter;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shooter.TakeDamage(true, damage);
            Destroy(gameObject);
        }
    }
}