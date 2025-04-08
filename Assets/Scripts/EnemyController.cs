using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] int damageToPlayer = 20;
    [SerializeField] bool obstacle = true;
    [SerializeField] float scoreValue = 50f;
    public EnemyPool enemyPool;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || (other.CompareTag("Bullet") && obstacle))
        {
            GameManager.Instance?.AddPoints(scoreValue);
            enemyPool.ReturnEnemy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage(damageToPlayer);
            enemyPool.ReturnEnemy(gameObject);
        }
    }
}