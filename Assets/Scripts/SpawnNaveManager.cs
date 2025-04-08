using UnityEngine;

public class SpawnNaveManager : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPosition = new Vector2(-8.54f, 0.52f);

    void Start()
    {
        if (StatsPlayers.naveSeleccionada != null && StatsPlayers.naveSeleccionada.navePrefab != null)
        {
            GameObject instantiatedShip = Instantiate(StatsPlayers.naveSeleccionada.navePrefab, spawnPosition, Quaternion.identity);

            SpriteRenderer sr = instantiatedShip.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = StatsPlayers.naveSeleccionada.shipColor;
            }
        }
        else
        {
            Debug.LogWarning("No se ha seleccionado ninguna nave o el prefab es nulo.");
        }
    }
}
