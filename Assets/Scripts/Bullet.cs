using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Daño")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if(!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}