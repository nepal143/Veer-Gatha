using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.TakeDamage(true, damage);
            }
            Destroy(gameObject, 2f);
        }
    }
} 
