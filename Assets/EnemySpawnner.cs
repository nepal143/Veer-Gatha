using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f;
    public float enemySpeed = 5f; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return; // Avoid errors if no spawn points

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        newEnemy.AddComponent<EnemyMovement>().speed = enemySpeed; // Add movement script dynamically
    }
}
