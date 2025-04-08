using UnityEngine;

[CreateAssetMenu(fileName = "StatsPlayers", menuName = "Scriptable Objects/StatsPlayers")]
public class StatsPlayers : ScriptableObject
{
    [Header("Stats de la Nave")]
    public float maxHealth = 100f;
    public float speedY = 5f;
    public float scoreSpeed = 1f;

    [Header("Cadencia y Disparo")]
    public float fireRate = 0.2f;
    public float bulletSpeed = 20f;
    [Header("Visuales")]
    public GameObject navePrefab;
    public Color shipColor = Color.white;

    public static StatsPlayers naveSeleccionada;
}
