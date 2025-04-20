using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Referencias")]
    public GyroMovement player;
    public StatsPlayers playerStats;
    public EnemyPool enemyPool;

    [Header("Datos del Jugador")]
    public PlayerDataSO playerData;
    public ScoreRecordSO scoreRecord;

    [Header("Eventos")]
    public UnityEvent<float> onScoreUpdated;
    public UnityEvent<float> onHealthUpdated;

    private UI_Manager uiManager;
    private bool isGameActive;

    private void Awake()
    {
        uiManager = GetComponent<UI_Manager>();
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (StatsPlayers.naveSeleccionada != null)
        {
            playerStats = StatsPlayers.naveSeleccionada;
        }

        if (playerData != null && playerStats != null)
        {
            playerData.currentHealth = playerStats.maxHealth;
            playerData.currentScore = 0f;

            if (player != null)
            {
                player.stats = playerStats;
                var sr = player.GetComponent<SpriteRenderer>();
                if (sr != null) sr.color = playerStats.shipColor;
            }

            UpdateHealthUI(playerData.currentHealth);
            isGameActive = true;
        }
    }

    private void Update()
    {
        if (!isGameActive) return;

        if (playerData != null && playerStats != null)
        {
            float effectiveSpeed = Mathf.Max(0, playerStats.scoreSpeed);
            playerData.currentScore += effectiveSpeed * Time.deltaTime;
            UpdateScoreUI(playerData.currentScore);
        }
    }

    public void AddPoints(float points)
    {
        if (!isGameActive) return;

        float pointsToAdd = Mathf.Max(0, points);
        playerData.currentScore += pointsToAdd;
        UpdateScoreUI(playerData.currentScore);

        Debug.Log($"Puntos añadidos: {pointsToAdd}. Score total: {playerData.currentScore}");
    }
    public void TakeDamage(int damage)
    {
        if (!isGameActive) return;

        playerData.currentHealth = Mathf.Max(0, playerData.currentHealth - damage);
        UpdateHealthUI(playerData.currentHealth);
        Debug.Log("Si collisona con el enemigo");
        if (playerData.currentHealth <= 0)
        {
            HandlePlayerDeath();
        }
    }

    private void UpdateScoreUI(float score)
    {
        uiManager?.UpdateScore(score);
        onScoreUpdated?.Invoke(score);
    }

    private void UpdateHealthUI(float health)
    {
        uiManager?.UpdateHealth(health);
        onHealthUpdated?.Invoke(health);
    }

    private void HandlePlayerDeath()
    {
        isGameActive = false;
        SceneGlobalManager.Instance?.ShowResults(); 
        player?.gameObject.SetActive(false);
    }
}