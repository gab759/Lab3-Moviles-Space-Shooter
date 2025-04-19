using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Referencias")]
    public StatsPlayers stats;
    [SerializeField] private Transform firePoint;

    private BulletPool bulletPool;
    private float nextFireTime = 0f;

    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + stats.fireRate;
            }
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + stats.fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPool == null || firePoint == null)
        {
            Debug.LogWarning("Referencias faltantes en PlayerShooting");
            return;
        }

        GameObject bullet = bulletPool.GetBullet(firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * stats.bulletSpeed;
        }
    }
}
