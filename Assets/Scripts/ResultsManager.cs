using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + GameManager.finalScore.ToString();
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
