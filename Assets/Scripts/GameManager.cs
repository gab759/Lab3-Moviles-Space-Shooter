using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referencias")]
    public UI_Manager uiManager;
    public GyroMovement player;
    public StatsPlayers playerStats;

    [Header("Variables")]
    public float currentHealth;
    private float score;
    public static int finalScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (StatsPlayers.naveSeleccionada != null)
        {
            playerStats = StatsPlayers.naveSeleccionada;
        }

        // Configurar al jugador
        if (player != null && playerStats != null)
        {
            player.stats = playerStats; // Asignar SO al GyroMovement
            currentHealth = playerStats.maxHealth;
            uiManager.UpdateHealth(currentHealth);
        }
    }

    void Update()
    {
        // Actualizar puntaje basado en el SO.
        score += playerStats.scoreSpeed * Time.deltaTime;
        uiManager.UpdateScore(score);
        finalScore = Mathf.FloorToInt(score);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        uiManager.UpdateHealth(currentHealth);
    }
}