using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size = 10;
        [HideInInspector] public List<GameObject> objects = new List<GameObject>();
    }

    public Pool[] pools;

    void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            Pool pool = pools[i];
            for (int j = 0; j < pool.size; j++)
            {
                GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
                obj.SetActive(false);
                obj.transform.SetParent(transform);

                EnemyController enemyController = obj.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.enemyPool = this;
                }

                pool.objects.Add(obj);
            }
        }
    }

    public GameObject GetEnemy(int poolIndex, Vector2 position)
    {
        if (poolIndex < 0 || poolIndex >= pools.Length)
        {
            Debug.LogError("Índice de pool inválido");
            return null;
        }

        List<GameObject> objects = pools[poolIndex].objects;
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }

        Debug.LogWarning("Pool vacío. Aumenta el tamaño en el Inspector.");
        return null;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}