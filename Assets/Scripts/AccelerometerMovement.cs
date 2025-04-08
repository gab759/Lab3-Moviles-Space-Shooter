using UnityEngine;

public class AccelerometerMovement : MonoBehaviour
{
    public StatsPlayers stats;

    private float minY = -3.48f;
    private float maxY = 5.48f;

    void Update()
    {
        float tilt = Input.acceleration.y;
        Vector3 newPosition = transform.position + Vector3.up * tilt * stats.speedY;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }
}
