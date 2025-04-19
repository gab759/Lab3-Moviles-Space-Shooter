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
}
