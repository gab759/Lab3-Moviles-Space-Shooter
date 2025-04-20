using UnityEngine;

[CreateAssetMenu(fileName = "ScoreRecordSO", menuName = "Scriptable Objects/ScoreRecordSO")]
public class ScoreRecordSO : ScriptableObject

{
    public float maxScore = 0;
    public float minScore = Mathf.Infinity;

    public void UpdateScores(float currentScore)
    {
        if (currentScore > maxScore) maxScore = currentScore;
        if (currentScore < minScore) minScore = currentScore;
    }
}