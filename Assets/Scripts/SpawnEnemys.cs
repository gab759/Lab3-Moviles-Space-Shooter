using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    [Header("Configuración")]
    public EnemyPool enemyPool;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minY = -3.48f;
    [SerializeField] private float maxY = 5.48f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPool == null)
        {
            Debug.LogWarning("EnemyPool no asignado en SpawnEnemys");
            return;
        }

        Vector2 spawnPosition = new Vector2(
            transform.position.x,
            Random.Range(minY, maxY)
        );

        Quaternion rotation = Quaternion.Euler(0f, 0f, 180f);
        enemyPool.GetEnemy(spawnPosition, rotation);
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = Mathf.Max(0.1f, newInterval);
    }
}