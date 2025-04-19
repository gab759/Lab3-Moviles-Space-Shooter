using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewEnemy();
        }
    }

    private GameObject CreateNewEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.SetActive(false);

        EnemyController controller = enemy.GetComponent<EnemyController>();
        if (controller != null)
        {
            controller.SetReferences(gameManager, this);
        }

        enemyPool.Enqueue(enemy);
        return enemy;
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy;

        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
        }
        else
        {
            enemy = CreateNewEnemy();
        }

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}