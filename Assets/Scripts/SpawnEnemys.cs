using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemyPrefab;
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
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(transform.position.x, randomY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}