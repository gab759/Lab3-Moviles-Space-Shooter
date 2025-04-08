using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Daño")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 2f;
    private float currentLifetime;
    private BulletPool bulletPool;

    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    void OnEnable()
    {
        currentLifetime = lifetime;
    }

    void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0f && bulletPool != null)
        {
            bulletPool.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.TakeDamage(damage);
            if (bulletPool != null) bulletPool.ReturnBullet(gameObject);
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            if (bulletPool != null) bulletPool.ReturnBullet(gameObject);
        }
    }
}