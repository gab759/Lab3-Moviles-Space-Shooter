using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMenu()
    {
        if (SceneGlobalManager.Instance != null)
        {
            SceneGlobalManager.Instance.LoadMenu();
        }
        else
        {
            Debug.LogError("SceneGlobalManager no está inicializado.");
            SceneManager.LoadScene("Menu");
        }
    }

    public void LoadCharacterSelection()
    {
        if (SceneGlobalManager.Instance != null)
        {
            SceneGlobalManager.Instance.LoadCharacterSelect();
        }
        else
        {
            Debug.LogError("SceneGlobalManager no está inicializado.");
            SceneManager.LoadScene("CharacterSelect");
        }
    }

    public void LoadMainGame()
    {
        if (SceneGlobalManager.Instance != null)
        {
            SceneGlobalManager.Instance.LoadGame();
        }
        else
        {
            Debug.LogError("SceneGlobalManager no está inicializado.");
            SceneManager.LoadScene("Game");
        }
    }
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
