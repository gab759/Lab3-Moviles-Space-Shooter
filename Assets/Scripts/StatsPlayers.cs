using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StatsPlayers", menuName = "Scriptable Objects/StatsPlayers")]
public class StatsPlayers : ScriptableObject
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float speedY = 5f;
    public float scoreSpeed = 1f;

    public GameObject navePrefab;
    public static StatsPlayers naveSeleccionada;

}