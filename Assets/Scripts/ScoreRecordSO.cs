using UnityEngine;

[CreateAssetMenu(fileName = "ScoreRecordSO", menuName = "Scriptable Objects/ScoreRecordSO")]
public class ScoreRecordSO : ScriptableObject
{
    public float maxScore = 0;
    public float minScore = Mathf.Infinity;
    public float lastScore = 0;

    /// <summary>
    /// Actualiza los r�cords si el nuevo score es mayor o menor
    /// Devuelve true si se actualiz� alg�n r�cord
    /// </summary>
    public bool TryRegisterNewScore(float newScore)
    {
        bool recordUpdated = false;
        lastScore = newScore;

        if (newScore > maxScore)
        {
            maxScore = newScore;
            recordUpdated = true;
        }

        if (newScore < minScore)
        {
            minScore = newScore;
            recordUpdated = true;
        }

        return recordUpdated;
    }

    /// <summary>
    /// Versi�n simplificada que solo actualiza los valores
    /// </summary>
    public void UpdateScores(float currentScore)
    {
        TryRegisterNewScore(currentScore);
    }

    /// <summary>
    /// Reinicia todos los scores (opcional)
    /// </summary>
    public void ResetScores()
    {
        maxScore = 0;
        minScore = Mathf.Infinity;
        lastScore = 0;
    }
}