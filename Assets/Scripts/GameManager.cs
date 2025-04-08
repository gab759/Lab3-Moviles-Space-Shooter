using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referencias")]
    public UI_Manager uiManager;
    public GyroMovement player;
    public StatsPlayers playerStats;

    [Header("Datos del Jugador")]
    public PlayerDataSO playerData;
    public ScoreRecordSO scoreRecord;

    private void Awake()
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

        if (playerStats != null && playerData != null)
        {
            playerData.currentHealth = playerStats.maxHealth;
            playerData.currentScore = 0f;

            if (player != null)
            {
                player.stats = playerStats;
                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = playerStats.shipColor;
                }
            }

            uiManager.UpdateHealth(playerData.currentHealth);
        }
    }

    void Update()
    {
        if (playerData != null && playerStats != null)
        {
            playerData.currentScore += playerStats.scoreSpeed * Time.deltaTime;
            uiManager.UpdateScore(playerData.currentScore);
        }
    }

    public float CurrentHealth
    {
        get { return (playerData != null) ? playerData.currentHealth : 0f; }
    }

    public void TakeDamage(int damage)
    {
        if (playerData != null)
        {
            playerData.currentHealth -= damage;
            if (playerData.currentHealth < 0)
                playerData.currentHealth = 0;

            uiManager.UpdateHealth(playerData.currentHealth);
        }
    }

    public void AddPoints(float points)
    {
        if (playerData != null)
        {
            playerData.currentScore += points;
            uiManager.UpdateScore(playerData.currentScore);
        }
    }
}
