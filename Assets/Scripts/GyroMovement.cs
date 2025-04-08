using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    public StatsPlayers stats;
    private Gyroscope gyro;
    private bool gyroEnabled = false;

    private float minY = -3.48f;
    private float maxY = 5.48f;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroEnabled = true;
        }
        else
        {
            Debug.LogWarning("Giroscopio no disponible. Usando controles alternativos.");
        }
    }

    void Update()
    {
        if (!gyroEnabled) return;

        float tilt = gyro.rotationRateUnbiased.x;
        Vector3 newPosition = transform.position + Vector3.up * tilt * stats.speedY * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }
}
