using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] NotificationScores scoreNotifier;
    [Header("Referencias")]
    public GyroMovement player;
    public StatsPlayers playerStats;
    public EnemyPool enemyPool;
    public TextMeshProUGUI uiText;
    [Header("Datos del Jugador")]
    public PlayerDataSO playerData;

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
    void Start()
    {
        WebGLInput.captureAllKeyboardInput = true; // Lo dejas activo por defecto
    }

    // Puedes agregar métodos públicos para cambiarlo desde JS si quieres:
    public void EnableKeyboardInput()
    {
        WebGLInput.captureAllKeyboardInput = true;
    }

    public void DisableKeyboardInput()
    {
        WebGLInput.captureAllKeyboardInput = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void ChangeText(string text)
    {   
        uiText.text = text;
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
            scoreNotifier.SetCurrentScore(playerData.currentScore);
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