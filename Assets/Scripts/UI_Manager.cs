using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    public void UpdateScore(float newScore)
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(newScore).ToString();

    }

    public void UpdateHealth(float newHealth)
    {
        healthText.text = "Life: " + Mathf.FloorToInt(newHealth).ToString();
    }
}