using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    public StatsPlayers stats;

    private float minY = -3.15f;
    private float maxY = 5.14f;

    void Update()
    {
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        Vector3 newPosition = transform.position + Vector3.up * verticalInput * stats.speedY * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }
}