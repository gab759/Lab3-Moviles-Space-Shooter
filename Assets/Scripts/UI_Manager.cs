using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private float score;
    private float health;

    public void UpdateScore(float newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    public void UpdateHealth(float newHealth)
    {
        health = newHealth;
        healthText.text = "Life: " + Mathf.FloorToInt(health).ToString();
    }
}