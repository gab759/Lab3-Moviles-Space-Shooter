using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI minScoreText;

    public PlayerDataSO playerData;
    public ScoreRecordSO scoreRecord;

    void Start()
    {
        float finalScore = playerData.currentScore;

        scoreRecord.UpdateScores(finalScore);

        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(finalScore).ToString();
        maxScoreText.text = "Max Score: " + Mathf.FloorToInt(scoreRecord.maxScore).ToString();
        minScoreText.text = "Min Score: " + Mathf.FloorToInt(scoreRecord.minScore).ToString();
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GoToCharacterSelection()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("CharacterSelection");
    }
}
