using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject deathEffect;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) Die();
    }

    private void Die()
    {
        if(deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}