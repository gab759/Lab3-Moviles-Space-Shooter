using UnityEngine;

[CreateAssetMenu(fileName = "StatsPlayers", menuName = "Scriptable Objects/StatsPlayers")]
public class StatsPlayers : ScriptableObject
{
    public float maxHealth = 100f;
    public float speedY = 5f;
    public float scoreSpeed = 1f;
}