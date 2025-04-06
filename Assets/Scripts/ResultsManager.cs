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
    }

    public void GoToCharacterSelection()
    {
        if (GameManager.Instance != null)
        {
            //LEE ESTO
            // Destruye el GameManager actual y limpia la instancia 
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null; // Importante para evitar referencia fantasma
        }
        SceneManager.LoadScene("CharacterSelection");
    }
}