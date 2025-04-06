using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private bool hasChangedScene = false;

    void Update()
    {
        if (!hasChangedScene && GameManager.Instance != null && GameManager.Instance.currentHealth <= 0f)
        {
            hasChangedScene = true;
            SceneManager.LoadScene("Results");
        }
    }
}