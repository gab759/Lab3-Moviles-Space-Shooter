using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    public StatsPlayers stats;         // ScriptableObject asignado desde el Inspector
    public UI_Manager uiManager;    // Referencia a la UI

    private Gyroscope gyro;
    private bool gyroEnabled;

    private float currentHealth;
    private float score;

    private float minY = -3.48f;
    private float maxY = 5.48f;

    void Start()    
    {
        currentHealth = stats.maxHealth;
        if (uiManager != null)
        {
            uiManager.UpdateHealth(currentHealth);
        }
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroEnabled = true;
        }
        else
        {
            Debug.LogWarning("Giroscopio no disponible.");
        }
    }
    public void TakeDamage(int damage)
    {
        stats.maxHealth -= damage;
        stats.maxHealth = Mathf.Max(stats.maxHealth, 0);

        if (uiManager != null)
            uiManager.UpdateHealth(stats.maxHealth);

        Debug.Log("Vida actual: " + stats.maxHealth);
    }
    void Update()
    {
        if (!gyroEnabled) return;

        // Movimiento vertical
        float tilt = gyro.rotationRateUnbiased.x;
        Vector3 newPosition = transform.position + Vector3.up * tilt * stats.speedY * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        // Sumar puntos según la velocidad del SO
        score += stats.scoreSpeed * Time.deltaTime;

        // Actualizar UI
        if (uiManager != null)
            uiManager.UpdateScore(score);
    }
}