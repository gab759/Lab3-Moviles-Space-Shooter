using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] int damageToPlayer = 20;

    void Update()
    {
        // Movimiento hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("GAAAAAAAAAA");
            GyroMovement player = other.GetComponent<GyroMovement>();

            if (other.CompareTag("Player"))
            {
                GameManager.Instance.TakeDamage(damageToPlayer); // ¡Nueva llamada!
                Destroy(gameObject);
            }
        }
    }
}