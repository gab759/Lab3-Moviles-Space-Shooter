using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damageToPlayer = 20;
    [SerializeField] private bool obstacle = true;
    [SerializeField] private float scoreValue = 50f;

    private GameManager _gameManager;
    private EnemyPool _enemyPool;

    public void SetReferences(GameManager gm, EnemyPool pool)
    {
        _gameManager = gm;
        _enemyPool = pool;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || (other.CompareTag("Bullet") && obstacle))
        {
            _gameManager?.AddPoints(scoreValue);
            _enemyPool?.ReturnEnemy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            _gameManager?.TakeDamage(damageToPlayer);
            _enemyPool?.ReturnEnemy(gameObject);
        }
    }
}