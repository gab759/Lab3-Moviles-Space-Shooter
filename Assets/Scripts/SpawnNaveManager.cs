using UnityEngine;

public class SpawnNaveManager : MonoBehaviour
{
    // Coordenadas 2D para el spawn (x, y)
    private Vector2 spawnPosition = new Vector2(-8.54f, 0.52f);

    void Start()
    {
        if (StatsPlayers.naveSeleccionada != null && StatsPlayers.naveSeleccionada.navePrefab != null)
        {
            // Instanciar la nave en posición 2D (Unity convierte Vector2 a Vector3 automáticamente)
            Instantiate(StatsPlayers.naveSeleccionada.navePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No se seleccionó nave. Instanciando nave por defecto.");
            // Opcional: Instanciar prefab por defecto en spawnPosition
        }
    }
}