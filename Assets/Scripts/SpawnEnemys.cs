using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public EnemyPool enemyPool;
    [SerializeField] float spawnInterval = 2f;

    private float minY = -3.48f;
    private float maxY = 5.48f;
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
        int randomPoolIndex = Random.Range(0, enemyPool.pools.Length);
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(minY, maxY));
        enemyPool.GetEnemy(randomPoolIndex, spawnPosition);
    }
}