using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    public float speed = 5f;
    float minY = -3.48f;
    float maxY = 5.48f;

    private Gyroscope gyro;
    private bool gyroEnabled;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroEnabled = true;
            Debug.Log("Gyroscope enable");

        }
        else
        {
            Debug.LogWarning("Giroscopio no disponible en este dispositivo.");
        }
    }

    void Update()
    {
        if (!gyroEnabled) return;

        float tilt = gyro.rotationRateUnbiased.x;

        // Movimiento solo en Y
        Vector3 newPosition = transform.position + Vector3.up * tilt * speed * Time.deltaTime;

        // Limitar el movimiento en Y
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Mantener X y Z constantes
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}