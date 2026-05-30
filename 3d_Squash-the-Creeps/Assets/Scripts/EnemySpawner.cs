using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;     // Enemy Prefab im Inspector zuweisen
    [SerializeField] float spawnInterval = 2f;   // alle 2 Sekunden ein neuer Enemy
    [SerializeField] float spawnRadius = 10f;    // wie weit vom Mittelpunkt entfernt

    private float timer = 0f; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Zufälligen Punkt am Rand eines Kreises berechnen
        Vector2 randomCircle = Random.insideUnitCircle.normalized; // normalized = am Rand
        Vector3 spawnPos = new Vector3(randomCircle.x, 0.053f, randomCircle.y) * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}