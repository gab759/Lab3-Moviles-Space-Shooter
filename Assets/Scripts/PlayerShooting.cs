using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Referencias")]
    public StatsPlayers stats;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource shootingAudio;

    private BulletPool bulletPool;
    private float nextFireTime = 0f;
    private bool isTouching = false;

    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    void Update()
    {
        HandleTouchInput();
        HandleShootingAudio();
    }

    private void HandleTouchInput()
    {
        isTouching = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                isTouching = true;
                TryToShoot();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            isTouching = true;
            TryToShoot();
        }
    }

    private void HandleShootingAudio()
    {
        try
        {
            if (shootingAudio == null) return;

            if (isTouching)
            {
                if (!shootingAudio.isPlaying)
                    shootingAudio.Play();
            }
            else
            {
                if (shootingAudio.isPlaying)
                    shootingAudio.Stop();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Audio no disponible en {gameObject.name}: {e.Message}");
        }
    }

    private void TryToShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + stats.fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPool == null || firePoint == null)
            return;

        GameObject bullet = bulletPool.GetBullet(firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * stats.bulletSpeed;
        }
    }
}