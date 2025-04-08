using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject bulletPrefab;
    public int initialPoolSize = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void Start()
    {
        InitializePool();
        LinkToPlayerShooting();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        bullet.SetActive(false);
        bullet.transform.SetParent(transform);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetBulletPool(this);
        }

        bulletPool.Enqueue(bullet);
        return bullet;
    }

    private void LinkToPlayerShooting()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerShooting shooting = player.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.SetBulletPool(this);
            }
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count == 0)
        {
            Debug.Log("Creando bala adicional (pool dinámico)");
            CreateNewBullet();
        }

        GameObject bullet = bulletPool.Dequeue();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}